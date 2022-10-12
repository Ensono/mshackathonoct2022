resource "azurerm_application_insights" "this" {
  name                = "mshackathonoct2022mlw-ai"
  location            = var.location
  resource_group_name = var.resource_group
  application_type    = "web"
}

resource "azurerm_key_vault" "this" {
  name                = "mshackathonoct2022mlw-kv"
  location            = var.location
  resource_group_name = var.resource_group
  tenant_id           = data.azurerm_client_config.current.tenant_id
  sku_name            = "standard"
}

resource "azurerm_storage_account" "this" {
  name                     = "mshackathonoct2022mlwst"
  location                 = var.location
  resource_group_name      = var.resource_group
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_container_registry" "this" {
  name                = "mshackathonoct2022mlcr"
  resource_group_name = var.resource_group
  location            = var.location
  sku                 = "Standard"
  admin_enabled       = false
}

resource "azurerm_machine_learning_workspace" "this" {
  name                    = "mshackathonoct2022mlw"
  location                = var.location
  resource_group_name     = var.resource_group
  application_insights_id = azurerm_application_insights.this.id
  key_vault_id            = azurerm_key_vault.this.id
  storage_account_id      = azurerm_storage_account.this.id
  container_registry_id   = azurerm_container_registry.this.id
  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_service_plan" "this" {
  name                = "mshackathonoct2022-asp"
  resource_group_name = var.resource_group
  location            = var.location
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "this" {
  name                = "mshackathonoct2022-be"
  resource_group_name = var.resource_group
  location            = var.location
  service_plan_id     = azurerm_service_plan.this.id

  site_config {}
}