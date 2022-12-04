using Zx;

public class Project : ConsoleAppBase
{
    public async Task PackageVersion()
    {
        await "nbgv get-version -v NuGetPackageVersion";
    }
}