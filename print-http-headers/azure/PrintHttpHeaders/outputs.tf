output "function_app_name" {
  value       = azurerm_linux_function_app.function_app.name
  description = "Deployed function app name"
}

output "function_app_url" {
  value       = "http://${azurerm_linux_function_app.function_app.default_hostname}/api/root"
  description = "Deployed function app url"
}