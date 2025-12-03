variable "aws_region" {
  default = "eu-central-1"
}
variable "db_password" {
  type      = string
  sensitive = true
}
locals {
  env = terraform.workspace == "default" ? "dev" : terraform.workspace
  name_prefix = "userapi-${local.env}"
}