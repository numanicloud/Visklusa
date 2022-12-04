using Microsoft.Extensions.Options;

public class Config : ConsoleAppBase
{
    private readonly IOptions<Configuration> _config;

    public Config(IOptions<Configuration> config)
    {
        _config = config;
    }

    public async Task New()
    {
        var text = $@"{{
    ""{nameof(Configuration.LocalNuGetPath)}"": """",
    ""{nameof(Configuration.NuGetApiKey)}"": """",
    ""{nameof(Configuration.ProjectPathsToPack)}"": [],
    ""{nameof(Configuration.AppProjectPaths)}"": []
}}";

        await using var file = File.Create("appsettings.json");
        await using var textWriter = new StreamWriter(file);
        await textWriter.WriteLineAsync(text);
    }

    public void Show()
    {
        Console.WriteLine($"{nameof(Configuration.LocalNuGetPath)}: {_config.Value.LocalNuGetPath}");
        Console.WriteLine($"{nameof(Configuration.NuGetApiKey)}: {_config.Value.NuGetApiKey}");

        var projects = string.Join(",\n\t", _config.Value.ProjectPathsToPack);
        Console.WriteLine($"{nameof(Configuration.ProjectPathsToPack)}: [{projects}]");

        var apps = string.Join(",\n\t", _config.Value.AppProjectPaths);
        Console.WriteLine($"{nameof(Configuration.AppProjectPaths)}: [{apps}]");
    }
}