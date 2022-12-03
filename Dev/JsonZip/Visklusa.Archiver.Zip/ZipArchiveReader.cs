using System.IO;
using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip;

public class ZipArchiveReader : IArchiveReader
{
	private readonly ZipArchive _zip;

	public ZipArchiveReader(string packagePath)
	{
		var file = File.OpenRead(packagePath);
		_zip = new ZipArchive(file, ZipArchiveMode.Read);
	}

	public IAssetReader GetAsset(string assetName)
	{
		return new ZipAssetReader(_zip, assetName);
	}

	public void Dispose()
	{
		_zip.Dispose();
	}
}