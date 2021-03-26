using System;
using System.Text.Json;
using Visklusa.Abstraction.Archiver;
using Visklusa.Abstraction.Notation;
using Visklusa.Abstraction.Variant;
using Visklusa.Archiver.Zip;
using Visklusa.Notation.Json;

namespace Visklusa.JsonZip
{
	public class JsonZipVariant : IVisklusaVariant
	{
		private readonly string _packagePath;
		private readonly JsonCapabilityRepository _repository;
		private Func<JsonSerializerOptions, JsonSerializerOptions> _optionModifier;

		public string LayoutFileName { get; } = "layout.json";

		public JsonZipVariant(string packagePath, JsonCapabilityRepository repository)
		{
			_packagePath = packagePath;
			_repository = repository;
			_optionModifier = options => options;
		}

		public void SetOptionModifier(Func<JsonSerializerOptions, JsonSerializerOptions> selector)
		{
			_optionModifier = selector;
		}

		public IArchiveReader GetPackageReader()
		{
			return new ZipArchiveReader(_packagePath);
		}

		public IArchiveWriter GetPackageWriter()
		{
			return new ZipArchiveWriter(_packagePath);
		}

		public IDeserializer GetDeserializer()
		{
			var result = new JsonLayoutSerializer(_repository);
			result.Options = _optionModifier(result.Options);
			return result;
		}

		public ISerializer GetSerializer()
		{
			var result = new JsonLayoutSerializer(_repository);
			result.Options = _optionModifier(result.Options);
			return result;
		}
	}
}
