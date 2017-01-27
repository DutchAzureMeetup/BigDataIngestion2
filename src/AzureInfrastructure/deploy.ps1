#Login-AzureRmAccount

#Select-AzureRmSubscription -SubscriptionName "name_of_the_subscription"

& $PSScriptRoot\Deploy-AzureResourceGroup.ps1 `
    -ResourceGroupLocation "West Europe" `
    -ResourceGroupName "DAMBigData" `
    -TemplateFile "azuredeploy.json" `
    -TemplateParametersFile "azuredeploy.parameters.json"