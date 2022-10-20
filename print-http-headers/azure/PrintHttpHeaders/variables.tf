variable "az_region" {
  description = "Azure region for all resources."
  type        = string
  default     = "uksouth"
}

variable "app_name" {
  description = "Name of the application."
  type        = string
  default     = "print-http-headers"
}