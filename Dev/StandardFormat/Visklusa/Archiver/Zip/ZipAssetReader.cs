using System.IO.Compression;
using Visklusa.Common;

namespace Visklusa.Archiver.Zip
{
	internal class ZipAssetReader : IAssetReader
	{
		private readonly ZipArchive _zip;
		public string FilePath { get; }

		public ZipAssetReader(ZipArchive zip, string filePath)
		{
			_zip = zip;
			FilePath = filePath;
		}

		public byte[] Read()
		{
			if (_zip.GetEntry(FilePath) is not {} entry)
			{
				return new byte[0];
			}

			using var stream = entry.Open();
			return Helpers.ReadToEnd(stream);
		}
	}
}
