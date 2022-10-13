variable "az_region" {
  description = "Azure region for all resources."
  type        = string
  default     = "uksouth"
}

variable "rg_name" {
  description = "Name of the resource group."
  type        = string
  default     = "print-http-headers-rg"
}

variable "storage_account_name" {
  description = "Name of the storage account."
  type        = string
  default     = "printhttpheadersstorage"
}

variable "application_insights_name" {
  description = "Name of the application insights instance."
  type        = string
  default     = "print-http-headers-application-insights"
}

variable "app_service_plan_name" {
  description = "Name of the application service plan."
  type        = string
  default     = "print-http-headers-app-service-plan"
}

variable "function_app_name" {
  description = "Name of the function app."
  type        = string
  default     = "print-http-headers-function-app"
}

variable "binaries_publish_path" {
  description = "The path where the built binaries are published to."
  type        = string
  default     = "bin/publish"
}

variable "binaries_zip_path" {
  description = "The path where the built binaries are zipped to."
  type        = string
  default     = "bin/PrintHttpHeaders.zip"
}
