using System.IO;
using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip
{
	public class ZipArchiveReader : IArchiveReader
	{
		private readonly FileStream _file;
		private readonly ZipArchive _zip;

		public ZipArchiveReader(string packagePath)
		{
			_file = File.OpenRead(packagePath);
			_zip = new ZipArchive(_file, ZipArchiveMode.Read);
		}

		public IAssetReader GetLayoutFile()
		{
			return new ZipAssetReader(_zip, "layout");
		}

		public IAssetReader GetAsset(string filePath)
		{
			return new ZipAssetReader(_zip, filePath);
		}

		public void Dispose()
		{
			_file.Dispose();
			_zip.Dispose();
		}
	}
}
