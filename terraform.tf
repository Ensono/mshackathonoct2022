terraform {
  backend "azurerm" {}
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.26.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "~> 2.29.0"
    }
  }
  required_version = "~> 1.3.1"
}

provider "azurerm" {
  features {}
}

provider "azuread" {
  tenant_id = "f88c76e1-2e79-4cd5-8b37-842f3f870d58"
}