$shortAppName = "Vitastic"
$projectName = "$shortAppName"

$webProjectFolder = "./src/$projectName.Web"
$webClientProjectFolder = "$webProjectFolder/ClientApp"
$testProjectFolder = "./tests/$projectName.Test"

$iisDirectoryProduction = "\\server2\DeployedApps\apps\$projectName"
$settingsDirectoryProduction = "\\server2\Servers\AppConfigs\$projectName"

$iisDirectoryTest = "\\server2\DeployedApps\apps\$($projectName)-Test"
$settingsDirectoryTest = "$settingsDirectoryProduction"
