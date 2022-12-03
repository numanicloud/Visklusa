using System;
using System.Collections.Generic;
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

		private JsonLayoutSerializer? _serializerCache;
		private ZipArchiveReader? _readerCache;
		private ZipArchiveWriter? _writerCache;

		public string LayoutFileName => "layout.json";

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

		public IArchiveReader GetPackageReader() =>
			_readerCache ??= new ZipArchiveReader(_packagePath);

		public IArchiveWriter GetPackageWriter() =>
			_writerCache ??= new ZipArchiveWriter(_packagePath);

		public IDeserializer GetDeserializer() => GetJsonSerializer();

		public ISerializer GetSerializer() => GetJsonSerializer();
		
		public IEnumerable<IAssetReader> GetAllAsset()
		{
			foreach (var assetName in LoadAllAssetNames())
			{
				yield return GetPackageReader().GetAsset(assetName);
			}
		}

		private JsonLayoutSerializer GetJsonSerializer()
		{
			if (_serializerCache is not null) return _serializerCache;
			
			_serializerCache = new JsonLayoutSerializer(_repository);
			_serializerCache.Options = _optionModifier(_serializerCache.Options);
			return _serializerCache;
		}

		private string[] LoadAllAssetNames()
		{
			var file = GetPackageReader()
				.GetAsset("assets.json")
				.Read();
			return GetJsonSerializer().DeserializeAsStrings(file);
		}
	}
}
