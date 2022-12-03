using System;
using System.IO.Compression;

namespace Visklusa.Archiver.Zip
{
	internal class ZipAssetWriter : IAssetWriter
	{
		private readonly ZipArchive _zip;
		public string FilePath { get; }

		public ZipAssetWriter(ZipArchive zip, string filePath)
		{
			_zip = zip;
			FilePath = filePath;
		}

		public void Write(ReadOnlySpan<byte> data)
		{
			var entry = _zip.CreateEntry(FilePath);
			using var stream = entry.Open();
			stream.Write(data);
		}
	}
}
