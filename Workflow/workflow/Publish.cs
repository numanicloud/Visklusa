﻿using Microsoft.Extensions.Options;
using Numani.TypedFilePath.Infrastructure;
using Zx;

public class Publish : ConsoleAppBase
{
    private readonly IOptions<Configuration> _config;

    public Publish(IOptions<Configuration> config)
    {
        _config = config;
    }
    
    public async Task Local(string projectPath)
    {
        var localNugetPath = _config.Value.LocalNuGetPath.AssertDirectoryPath();
        await PublishNuGetPackageAsync(projectPath,
            $"-c Debug --include-symbols",
            $"-s {localNugetPath.PathString}");
    }

    public async Task Nuget(string projectPath)
    {
        await PublishNuGetPackageAsync(projectPath,
            $"-c Release", $"-k {_config.Value.NuGetApiKey} -s https://api.nuget.org/v3/index.json");
    }

    public async Task LocalAll()
    {
        foreach (var projectPath in _config.Value.ProjectPathsToPack)
        {
            await Local(projectPath);
        }
    }

    public async Task NugetAll()
    {
        foreach (var projectPath in _config.Value.ProjectPathsToPack)
        {
            await Nuget(projectPath);
        }
    }

    public async Task Apps()
    {
        foreach (var projectPath in _config.Value.AppProjectPaths)
        {
            await $"dotnet publish {projectPath} -c Release -r win-x64 --self-contained -o Apps/";
        }
    }

    private async Task PublishNuGetPackageAsync(string projectPath, string packOption,
        string pushOption)
    {
        var path = projectPath.AssertFilePathExt();

        var version = await $"nbgv get-version -v NuGetPackageVersion";
        await $"dotnet pack {path.PathString} -o Pack/ -v minimal -p:PackageVersion={version} {packOption}";

        var projectName = Path.GetFileNameWithoutExtension(path.PathString)
            .AssertFilePath();
        await $"dotnet nuget push \"Pack/{projectName.PathString}.{version}.nupkg\" {pushOption}";
    }
}