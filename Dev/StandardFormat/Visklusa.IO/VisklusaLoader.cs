using System;
using System.Collections.Generic;
using Visklusa.Abstraction.Archiver;
using Visklusa.Abstraction.Semantics;
using Visklusa.Abstraction.Variant;

namespace Visklusa.IO
{
	public class VisklusaLoader : IDisposable
	{
		private readonly IVisklusaVariant _format;
		private readonly IArchiveReader _reader;

		public VisklusaLoader(IVisklusaVariant format)
		{
			_format = format;
			_reader = _format.GetPackageReader();
		}

		public IAssetReader GetAsset(string filePath)
		{
			return _reader.GetAsset(filePath);
		}

		public Layout GetLayout()
		{
			var deserializer = _format.GetDeserializer();
			var layout = _reader.GetAsset(_format.LayoutFileName);
			var bytes = layout.Read();
			return deserializer.Deserialize(bytes);
		}

		public IEnumerable<IAssetReader> GetAllAsset() => _format.GetAllAsset();

		public void Dispose()
		{
			_reader.Dispose();
		}
	}
}
