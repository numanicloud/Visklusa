public class Configuration
{
    public string LocalNuGetPath { get; set; } = default!;
    public string NuGetApiKey { get; set; } = default!;
    public string[] ProjectPathsToPack { get; set; } = Array.Empty<string>();
    public string[] AppProjectPaths { get; set; } = Array.Empty<string>();
}