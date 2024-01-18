data "azurerm_log_analytics_workspace" "analytics" {
  name                = "platform"
  resource_group_name = "rg-analytics"
}

data "azurerm_resource_group" "shared_services" {
  name = "rg-shared-services"
}

data "azurerm_resource_group" "applications" {
  name = "rg-container-apps"
}

data "azurerm_container_app_environment" "applications" {
  name                = "cae-applications"
  resource_group_name = data.azurerm_resource_group.applications.name
}

data "azurerm_container_registry" "acr" {
  name                = "crthunebyinfrastructure"
  resource_group_name = data.azurerm_resource_group.shared_services.name
}