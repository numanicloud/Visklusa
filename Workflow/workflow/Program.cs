using Microsoft.Extensions.DependencyInjection;

var builder = ConsoleApp.CreateBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<Configuration>(context.Configuration);
    });

var app = builder.Build();

app.AddSubCommands<Publish>();
app.AddSubCommands<Config>();
app.AddCommands<Project>();
app.Run();