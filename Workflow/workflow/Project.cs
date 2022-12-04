using Microsoft.Extensions.Options;
using Zx;

public class Project : ConsoleAppBase
{
    private readonly IOptions<Configuration> _options;

    public Project(IOptions<Configuration> options)
    {
        _options = options;
    }

    public async Task PackageVersion()
    {
        foreach (var projectPath in _options.Value.ProjectPathsToPack)
        {
            var projectName = Path.GetFileName(projectPath);
            Console.Write($"{projectName}: ");
            await $"nbgv get-version -p {projectPath} -v NuGetPackageVersion";
        }
    }
}