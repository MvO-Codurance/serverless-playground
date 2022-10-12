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

/*
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

variable "lambda_function_name" {
  description = "The name given to the Lambda function."
  type        = string
  default     = "print_http_headers_lambda"
}

variable "lambda_handler_name" {
  description = "The name given to the Lambda function handler."
  type        = string
  default     = "PrintHttpHeaders"
}

variable "lambda_iam_role_name" {
  description = "The name given to the Lambda function IAM role."
  type        = string
  default     = "print_http_headers_lambda_role"
}

variable "lambda_runtime" {
  description = "The runtime to use for the Lambda function."
  type        = string
  default     = "dotnet6"
}

variable "api_gateway_name" {
  description = "The name to use for the API Gateway."
  type        = string
  default     = "print_http_headers_gateway"
}

variable "api_gateway_stage_name" {
  description = "The stage name to use for the API Gateway."
  type        = string
  default     = "test"
}
*/