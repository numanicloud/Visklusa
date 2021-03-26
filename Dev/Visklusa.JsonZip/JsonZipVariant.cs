using Visklusa.Abstraction.Archiver;
using Visklusa.Abstraction.Notation;
using Visklusa.Abstraction.Variant;
using Visklusa.Archiver.Zip;
using Visklusa.Notation.Json;

namespace Visklusa.JsonZip
{
	internal class JsonZipVariant : IVisklusaVariant
	{
		private readonly string _packagePath;
		private readonly JsonCapabilityRepository _repository;

		public string LayoutFileName { get; } = "layout.json";

		public JsonZipVariant(string packagePath, JsonCapabilityRepository repository)
		{
			_packagePath = packagePath;
			_repository = repository;
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
			return new JsonLayoutSerializer(_repository);
		}

		public ISerializer GetSerializer()
		{
			return new JsonLayoutSerializer(_repository);
		}
	}
}
