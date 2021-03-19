using Visklusa.Archiver;
using Visklusa.Archiver.Zip;
using Visklusa.Notation;
using Visklusa.Notation.Json;

namespace Visklusa.Variant.ZipJson
{
	public class ZipJsonVariant : IFormat
	{
		private readonly string _packagePath;
		private readonly JsonCapabilityRepository _repository;

		public string LayoutFileName => "layout.json";

		public ZipJsonVariant(string packagePath, JsonCapabilityRepository repository)
		{
			_packagePath = packagePath;
			_repository = repository;
		}

		public IArchiveReader GetPackageReader() => new ZipArchiveReader(_packagePath);

		public IArchiveWriter GetPackageWriter() => new ZipArchiveWriter(_packagePath);

		public IDeserializer GetDeserializer() => new JsonLayoutSerializer(_repository);

		public ISerializer GetSerializer() => new JsonLayoutSerializer(_repository);
	}
}
