using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Cysharp.Diagnostics;
using Reactive.Bindings;
using Zx;

namespace Visklusa.Workflow.Model;

public interface IOutputTarget
{
    public void WriteLine(string message);
    public void WriteError(string error);
}

public class LauncherContext : IOutputTarget
{
    private readonly Dispatcher _uiDispatcher;
    public CommandViewModel PublishNuGetAllButton { get; }
    public CommandViewModel[] PublishNuGetButtons { get; }
    public CommandViewModel PublishLocalAllButton { get; }
    public CommandViewModel[] PublishLocalButtons { get; }
    
    public CommandViewModel[] CommandViewModels { get; }
    public ObservableCollection<OutputLine> Outputs { get; }

    public LauncherContext(Dispatcher uiDispatcher)
    {
        _uiDispatcher = uiDispatcher;
        var nugetApiKey = "somekey";
        var local = @"D:\Home\MyDocuments\Projects\MyNugetFeed\Visklusa";
        var projects = new[]
        {
            "../Dev/Visklusa.Domain/Visklusa.Domain.csproj",
            "../Dev/Visklusa.Domain2/Visklusa.Domain2.csproj"
        };

        var nuGetButtons = projects
            .Select(projectPath => new PublishNuGetButton
            {
                ProjectPath = projectPath,
                NugetApiKey = nugetApiKey,
                OutputTarget = this,
            })
            .ToArray();
        var nugetAllButton =
            new CombinationButton(nuGetButtons.OfType<ICommandDomain>().ToArray());

        var localButtons = projects
            .Select(projectPath => new PublishLocalButton
            {
                ProjectPath = projectPath,
                LocalPath = local,
                OutputTarget = this
            })
            .ToArray();
        var localAllButton =
            new CombinationButton(localButtons.OfType<ICommandDomain>().ToArray());

        PublishNuGetButtons = nuGetButtons
            .Select(x =>
            {
                var projectName = Path.GetFileNameWithoutExtension(x.ProjectPath);
                return new CommandViewModel($"Publish {projectName} to NuGet", x);
            })
            .ToArray();
        PublishNuGetAllButton = new CommandViewModel(
            "Publish all to NuGet",
            nugetAllButton);

        PublishLocalButtons = localButtons
            .Select(x =>
            {
                var projectName = Path.GetFileNameWithoutExtension(x.ProjectPath);
                return new CommandViewModel($"Publish {projectName} to local", x);
            })
            .ToArray();
        PublishLocalAllButton = new CommandViewModel(
            "Publish all to local",
            localAllButton);

        CommandViewModels = PublishLocalButtons
            .Append(PublishLocalAllButton)
            .Concat(PublishNuGetButtons)
            .Append(PublishNuGetAllButton)
            .ToArray();

        Outputs = new ObservableCollection<OutputLine>();
    }

    public void WriteLine(string message)
    {
        _uiDispatcher.Invoke(() =>
        {
            Outputs.Add(new OutputLine
            {
                Type = OutputType.Default.ToString(),
                Text = message
            });
        });
    }

    public void WriteError(string error)
    {
        _uiDispatcher.Invoke(() =>
        {
            Outputs.Add(new OutputLine
            {
                Type = OutputType.Error.ToString(),
                Text = error
            });
        });
    }
}

public class OutputLine
{
    public required string Type { get; init; }
    public required string Text { get; init; }
}

public enum OutputType
{
    Default, Warning, Error
}

public class CommandViewModel
{
    private readonly ReactiveProperty<bool> _isExecuting = new (false);
    
    public string Title { get; }
    public ReactiveCommand Command { get; }

    public CommandViewModel(string title, ICommandDomain commandDomain)
    {
        Title = title;
        Command = _isExecuting.Select(x => !x).ToReactiveCommand();
        Command.Subscribe(() => Task.Run(async () =>
        {
            _isExecuting.Value = true;

            await commandDomain.ExecuteAsync();
            
            _isExecuting.Value = false;
        }).ConfigureAwait(false));
    }
}

public interface ICommandDomain
{
    public Task ExecuteAsync();
}

public class PublishNuGetButton : ICommandDomain
{
    public required string NugetApiKey { get; init; }
    public required string ProjectPath { get; init; }
    public required IOutputTarget OutputTarget { get; init; }

    public async Task ExecuteAsync()
    {
        await Helpers.PackNuGetPackage(ProjectPath,
            $"-k {NugetApiKey} -s https://api.nuget.org/v3/index.json",
            OutputTarget);
    }
}

public class PublishLocalButton : ICommandDomain
{
    public required string ProjectPath { get; init; }
    public required string LocalPath { get; init; }
    public required IOutputTarget OutputTarget { get; init; }
    
    public async Task ExecuteAsync()
    {
        await Helpers.PackNuGetPackage(ProjectPath, $"-s {LocalPath}", OutputTarget);
    }
}

public class CombinationButton : ICommandDomain
{
    private readonly ICommandDomain[] _commands;

    public CombinationButton(ICommandDomain[] commands)
    {
        _commands = commands;
    }

    public async Task ExecuteAsync()
    {
        foreach (var command in _commands)
        {
            await command.ExecuteAsync();
        }
    }
}

public static class Helpers
{
    public static async Task PackNuGetPackage(string projectPath, string options, IOutputTarget output)
    {
		var version = await $"nbgv get-version -p {projectPath} -v NuGetPackageVersion";

		await RunCommandAsync(
			$"dotnet pack {projectPath} -o Pack/ -v minimal -p:PackageVersion={version}", output);

		var projectName = Path.GetFileNameWithoutExtension(projectPath);

		await RunCommandAsync(
			$"dotnet nuget push \"Pack/{projectName}.{version}\".nupkg {options}", output);
	}

	public static async Task RunCommandAsync(string command, IOutputTarget outputTarget)
	{
		var (process, stdOut, stdErr) = ProcessX.GetDualAsyncEnumerable(command, encoding: Encoding.UTF8);
		var consumeStdOut = Task.Run(async () =>
		{
			await foreach (var item in stdOut)
			{
				outputTarget.WriteLine(item);
			}
		});
		var consumeStdErr = Task.Run(async () =>
		{
			await foreach (var item in stdErr)
			{
                outputTarget.WriteError(item);
			}
		});

		await Task.WhenAny(process.WaitForExitAsync(), Task.WhenAll(consumeStdOut, consumeStdErr));
	}
}