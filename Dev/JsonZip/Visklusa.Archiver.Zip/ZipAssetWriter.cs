using System;
using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip
{
	public class ZipAssetWriter : IAssetWriter
	{
		private readonly ZipArchive _zip;
		public string AssetName { get; }

		public ZipAssetWriter(ZipArchive zip, string filePath)
		{
			_zip = zip;
			AssetName = filePath;
		}

		public void Write(ReadOnlySpan<byte> data)
		{
			var entry = _zip.CreateEntry(AssetName);
			using var stream = entry.Open();
			stream.Write(data);
		}
	}
}
