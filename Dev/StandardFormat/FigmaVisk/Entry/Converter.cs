using FigmaVisk.Capability;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Visklusa.Abstraction.Semantics;
using Visklusa.IO;
using Visklusa.JsonZip;
using Visklusa.Notation.Json;

namespace FigmaVisk.Entry;

internal class Converter
{
	private readonly FigmaApiAgent _agent;
	private readonly DocumentAnalyzer _documentAnalyzer;
	private readonly AltTransformAnalyzer _altTransformAnalyzer;
	private readonly ImageInstaller _imageInstaller;

	public Converter(FigmaApiAgent agent, DocumentAnalyzer documentAnalyzer,
		AltTransformAnalyzer altTransformAnalyzer, ImageInstaller imageInstaller)
	{
		_agent = agent;
		_documentAnalyzer = documentAnalyzer;
		_altTransformAnalyzer = altTransformAnalyzer;
		_imageInstaller = imageInstaller;
	}

	public async Task RunAsync(StartupOption option)
	{
		var elements = await ScanElementsAsync(option);
		var convertContext = GetConvertContext();
		var variant = GetVariant(option, convertContext.Repository);
		await SaveAsync(variant, elements, convertContext);
	}

	private async Task<NodeExport[]> ScanElementsAsync(StartupOption option)
	{
		var document = await _agent.Download(option);
		if (document is null)
		{
			throw new Exception();
		}

		var elements = _documentAnalyzer.Analyze(document);
		elements = _altTransformAnalyzer.Convert(elements);
		var installations = _imageInstaller.Convert(elements);

		return installations.Select(x => new NodeExport(x)).ToArray();
	}

	private static ConvertContext GetConvertContext()
	{
		return new ConvertContext()
			.Register<Visklusa.Preset.BoundingBox>()
			.Register<Visklusa.Preset.ZOffset>()
			.Register<Visklusa.Preset.FamilyShip>()
			.Register<Visklusa.Preset.Image>()
			.Register<Paint>()
			.Register<RoundedRectangle>()
			.Register<Text>()
			.Register<FigmaId>()
			.Register<AltPosition>()
			.Register<VerticalScroll>()
			.Register<VerticalList>();
	}

	private static JsonZipVariant GetVariant(StartupOption option, JsonCapabilityRepository repo)
	{
		var variant = new JsonZipVariant(option.OutputPath, repo);
		variant.SetOptionModifier(sOption =>
		{
			var result = new JsonSerializerOptions(sOption);
			result.IgnoreReadOnlyProperties = true;
			return result;
		});
		return variant;
	}

	private static async Task SaveAsync(JsonZipVariant variant, NodeExport[] elements,
		ConvertContext convertContext)
	{
		using var visk = new VisklusaSaver(variant);

		visk.AddLayout(new Layout(convertContext.GetAssertion(), elements.Select(x => x.Element).ToArray()));

		foreach (var item in elements)
		{
			await item.Installation.OnSaveAsync(visk);
		}
	}

	private record NodeExport(ImageInstaller.IImageInstallation Installation)
	{
		public Element Element => Installation.Element;
	}
}