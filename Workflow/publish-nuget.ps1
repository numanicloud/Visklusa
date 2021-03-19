param([String]$projectName)

$project = "..\Dev\${projectName}\${projectName}.csproj"

$version = nbgv get-version -p $project -v NuGetPackageVersion
dotnet pack $project -o Pack/ -v minimal -p:PackageVersion=$version

$key = Get-Content ./nugetapikey.txt

dotnet nuget push "Pack/${projectName}.${version}.nupkg" -k $key -s https://api.nuget.org/v3/index.json