$manifest = Get-Content ./Build/FFLogsLookup/FFLogsLookup.json | ConvertFrom-Json
$repository = Get-Content ./repository.json | ConvertFrom-Json
$repository[0].Author = $manifest.Author
$repository[0].Name = $manifest.Name
$repository[0].InternalName = $manifest.InternalName
$repository[0].AssemblyVersion = $manifest.AssemblyVersion
$repository[0].Description = $manifest.Description
$repository[0].ApplicableVersion = $manifest.ApplicableVersion
$repository[0].DalamudApiLevel = $manifest.DalamudApiLevel
$repository[0].Punchline = $manifest.Punchline
$repository[0].LastUpdated = [int](Get-Date -UFormat %s -Millisecond 0)
ConvertTo-Json @($repository[0]) -Depth 10 | Out-File ./repository.json -encoding utf8