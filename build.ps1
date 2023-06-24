# Step 1: Remove the bin directory
Remove-Item -Path ./bin -Recurse -Force

# Step 2: Remove any nupkg files
Remove-Item -Path ./*.nupkg -Force

# Step 3: Read the contents of the "version_info" file
$versionInfoPath = "${PSScriptRoot}/version_info"
$versionInfoContent = Get-Content -Path $versionInfoPath

# Step 4: Replace the version in the "SugarWorldNewsAPI.nuspec" file
$nuspecFilePath = "${PSScriptRoot}/SugarWorldNewsApiClient.nuspec"
$nuspecContent = Get-Content -Path $nuspecFilePath

$versionPattern = "<version>(.*?)<\/version>"
$newNuspecContent = $nuspecContent -replace $versionPattern, "<version>$versionInfoContent</version>"

# Step 5: Save the updated "SugarWorldNewsAPI.nuspec" file in-place
$newNuspecContent | Set-Content -Path $nuspecFilePath

# Step 5: Build for release
dotnet build --configuration Release

# Step 6: Pack for release
dotnet pack --configuration Release --include-symbols

# Step 7: Produce the nuget package
nuget pack SugarWorldNewsApiClient.csproj -Properties Configuration=Release

# Step 8: Update git repo
git add .
git commit -m "v${versionInfoContent}"
git push

# Step 9: Publish tags
git tag -a "v${versionInfoContent}" -s -m "Version ${versionInfoContent}"
git push origin "v${versionInfoContent}"
git push --tags

# Step 8: Publish updated package to Nuget.org
#dotnet nuget push *.nupkg --source https://api.nuget.org/v3/index.json --api-key oy2gv6u3s3lbkxfgyxdqwgf7ntljzocudapwlvjnjwqbaa