using System.IO;
using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip
{
	public class ZipArchiveWriter : IArchiveWriter
	{
		private readonly FileStream _file;
		private readonly ZipArchive _zip;

		public ZipArchiveWriter(string packagePath)
		{
			_file = File.Create(packagePath);
			_zip = new ZipArchive(_file, ZipArchiveMode.Create);
		}

		public IAssetWriter GetDocumentWriter()
		{
			return new ZipAssetWriter(_zip, "layout");
		}

		public IAssetWriter GetAssetWriter(string filePath)
		{
			return new ZipAssetWriter(_zip, filePath);
		}

		public void Dispose()
		{
			_file.Dispose();
			_zip.Dispose();
		}
	}
}
