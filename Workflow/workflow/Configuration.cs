public class Configuration
{
    public string LocalNuGetPath { get; set; } = default!;
    public string NuGetApiKey { get; set; } = default!;
    public string[] ProjectPathsToPack { get; set; } = new string[0];
}