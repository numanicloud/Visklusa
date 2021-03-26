param([String]$projectName)

$project = "..\Dev\${projectName}\${projectName}.csproj"

$version = nbgv get-version -p $project -v NuGetPackageVersion
dotnet pack $project -o Pack/ -v minimal -p:PackageVersion=$version

dotnet nuget push "Pack/${projectName}.${version}.nupkg" -s D:\Home\MyDocuments\Projects\MyNugetFeed\Visklusa