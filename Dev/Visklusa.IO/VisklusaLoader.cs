using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Visklusa.Abstraction.Archiver;
using Visklusa.Abstraction.Notation;
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

		public IEnumerable<IAssetReader> GetAllAsset()
		{
			var assetList = _reader.GetAsset("assets.json").Read();
			var json = Encoding.UTF8.GetString(assetList);
			var assets = JsonSerializer.Deserialize<string[]>(json);

			if (assets is null)
			{
				yield break;
			}

			foreach (var asset in assets)
			{
				yield return GetAsset(asset);
			}
		}

		public void Dispose()
		{
			_reader.Dispose();
		}
	}
}
