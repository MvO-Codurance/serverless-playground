variable "aws_region" {
  description = "AWS region for all resources."
  type        = string
  default     = "eu-west-2"
}

variable "app_name" {
  description = "Name of the application."
  type        = string
  default     = "print_http_headers"
}

variable "lambda_handler_name" {
  description = "The name given to the Lambda function handler."
  type        = string
  default     = "PrintHttpHeaders"
}
