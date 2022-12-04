using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FigmaVisk;

internal class ImageRetriever
{
	private readonly StartupOption _option;
	private Dictionary<string, byte[]> _imageCache = new ();
	private Dictionary<string, string>? _imageMap;

	public ImageRetriever(IOptions<StartupOption> option)
	{
		_option = option.Value;
	}

	[MemberNotNull(nameof(_imageMap))]
	public async Task LoadAsync()
	{
		using var http = new HttpClient();
		http.DefaultRequestHeaders.Add("X-FIGMA-TOKEN", _option.Token);

		var requestUri = $"https://api.figma.com/v1/files/{_option.FileId}/images";
		var response = await http.GetStringAsync(requestUri);

		JObject.Parse(response)["meta"]["images"].Children();

		var jsonElements = (JObject)JObject.Parse(response)["meta"]["images"];

		_imageMap = jsonElements.Properties()
			.ToDictionary(x => x.Name, x => (string)x.Value);
	}

	public async ValueTask<byte[]> DownloadAsync(string imageRef)
	{
		if (_imageMap is null)
		{
			await LoadAsync();
		}

		if (_imageCache.TryGetValue(imageRef, out var cached))
		{
			return cached;
		}

		Console.WriteLine($"Downloading image {imageRef}...");
		using var http = new HttpClient();
		var url = _imageMap[imageRef];
		return _imageCache[imageRef] = await http.GetByteArrayAsync(url);
	}
}