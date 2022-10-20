terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.26.0"
    }
    archive = {
      source  = "hashicorp/archive"
      version = "~> 2.2.0"
    }
  }

  required_version = ">= 1.2.0"
}

provider "azurerm" {
  features {
    resource_group {
      prevent_deletion_if_contains_resources = false
    }
  }
}

resource "azurerm_resource_group" "resource_group" {
  name     = "${var.app_name}-rg"
  location = var.az_region
}

resource "azurerm_storage_account" "storage_account" {
  name                     = "${replace(var.app_name, "-", "")}storage"
  resource_group_name      = azurerm_resource_group.resource_group.name
  location                 = var.az_region
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "storage_container" {
  name                  = "function-releases"
  storage_account_name  = azurerm_storage_account.storage_account.name
  container_access_type = "private"
}

data "archive_file" "archive_file" {
  type        = "zip"
  source_dir  = "bin/publish"
  output_path = "bin/${var.app_name}.zip"
}

resource "azurerm_storage_blob" "storage_blob" {
  name                   = "${filesha256(data.archive_file.archive_file.output_path)}.zip"
  storage_account_name   = azurerm_storage_account.storage_account.name
  storage_container_name = azurerm_storage_container.storage_container.name
  type                   = "Block"
  source                 = data.archive_file.archive_file.output_path
}

data "azurerm_storage_account_blob_container_sas" "storage_account_blob_container_sas" {
  connection_string = azurerm_storage_account.storage_account.primary_connection_string
  container_name    = azurerm_storage_container.storage_container.name
  start             = "2022-01-01T00:00:00Z"
  expiry            = "2032-01-01T00:00:00Z"
  permissions {
    read   = true
    add    = false
    create = false
    write  = false
    delete = false
    list   = false
  }
}

resource "azurerm_application_insights" "application_insights" {
  name                = "${var.app_name}-application-insights"
  location            = var.az_region
  resource_group_name = azurerm_resource_group.resource_group.name
  application_type    = "web"
}

resource "azurerm_service_plan" "app_service_plan" {
  name                = "${var.app_name}-service-plan"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = var.az_region
  os_type             = "Linux"
  sku_name            = "Y1"
}

resource "azurerm_linux_function_app" "function_app" {
  name                       = "${var.app_name}-function-app"
  resource_group_name        = azurerm_resource_group.resource_group.name
  location                   = var.az_region
  storage_account_name       = azurerm_storage_account.storage_account.name
  storage_account_access_key = azurerm_storage_account.storage_account.primary_access_key
  service_plan_id            = azurerm_service_plan.app_service_plan.id
  site_config {
    application_insights_key = azurerm_application_insights.application_insights.instrumentation_key
    use_32_bit_worker        = false
    application_stack {
      dotnet_version              = "6.0"
      use_dotnet_isolated_runtime = true
    }
  }
  builtin_logging_enabled = false
  app_settings = {
    "WEBSITE_RUN_FROM_PACKAGE" = "https://${azurerm_storage_account.storage_account.name}.blob.core.windows.net/${azurerm_storage_container.storage_container.name}/${azurerm_storage_blob.storage_blob.name}${data.azurerm_storage_account_blob_container_sas.storage_account_blob_container_sas.sas}"
  }
}
