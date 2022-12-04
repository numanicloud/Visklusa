using System;
using System.Threading;
using System.Threading.Tasks;
using FigmaVisk.Entry;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FigmaVisk;

internal class ConvertMain : IHostedService
{
	private readonly StartupOption _option;
	private readonly IHostApplicationLifetime _appLifetime;
	private readonly Converter _converter;

	public ConvertMain(IOptions<StartupOption> option, IHostApplicationLifetime appLifetime,
		Converter converter)
	{
		_option = option.Value;
		_appLifetime = appLifetime;
		_converter = converter;
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		try
		{
			_option.CheckOption();
		}
		catch (InvalidOperationException ex)
		{
			Console.WriteLine(ex.Message);
			_appLifetime.StopApplication();
			return;
		}
		
		var forget = RunAsync(cancellationToken);
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		return Task.CompletedTask;
	}

	private async Task RunAsync(CancellationToken ct)
	{
		await Task.Delay(1000, ct);
		await _converter.RunAsync(_option);
		_appLifetime.StopApplication();
	}
}