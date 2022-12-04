using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip
{
	public class ZipArchiveWriter : IArchiveWriter
	{
		private readonly ZipArchive _zip;
		private readonly Dictionary<string, ZipAssetWriter> _writers = new ();

		public ZipArchiveWriter(string packagePath)
		{
			var file = File.Create(packagePath);
			_zip = new ZipArchive(file, ZipArchiveMode.Update);
		}

		public IAssetWriter GetAssetWriter(string assetName)
		{
			if (_writers.TryGetValue(assetName, out var writer))
			{
				return writer;
			}
			
			return _writers[assetName] = new ZipAssetWriter(_zip, assetName);
		}

		public void Dispose()
		{
			_zip.Dispose();
		}
	}
}
