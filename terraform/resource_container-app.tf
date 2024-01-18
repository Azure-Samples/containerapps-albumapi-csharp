resource "azurecaf_name" "app_name" {
  name          = "albumapi"
  resource_type = "azurerm_container_app"
  clean_input   = true
}

resource "azurerm_container_app" "application" {
  name                         = azurecaf_name.app_name.result
  container_app_environment_id = data.azurerm_container_app_environment.applications.id
  resource_group_name          = data.azurerm_resource_group.applications.name
  revision_mode                = "Single"
  workload_profile_name        = "Consumption"

  template {
    container {
      name   = "examplecontainerapp"
      image  = "mcr.microsoft.com/azuredocs/containerapps-helloworld:latest"
      cpu    = 0.25
      memory = "0.5Gi"
      liveness_probe {
        port      = 80
        transport = "HTTP"
      }
    }
  }

  ingress {
    external_enabled = true
    target_port      = 80
    transport        = "auto"
    traffic_weight {
      percentage      = 100
      latest_revision = true
    }
  }

  lifecycle {
    ignore_changes = [
      tags
    ]
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_role_assignment" "container_registry_acrpull" {
  role_definition_name = "AcrPull"
  scope                = data.azurerm_container_registry.acr.id
  principal_id         = azurerm_container_app.application.identity[0].principal_id

  depends_on = [
    azurerm_container_app.application
  ]
}