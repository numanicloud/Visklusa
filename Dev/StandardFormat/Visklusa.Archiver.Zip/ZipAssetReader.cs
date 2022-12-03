using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip;

public class ZipAssetReader : IAssetReader
{
	private readonly ZipArchive _zip;
	public string AssetName { get; }

	public ZipAssetReader(ZipArchive zip, string filePath)
	{
		_zip = zip;
		AssetName = filePath;
	}

	public byte[] Read()
	{
		if (_zip.GetEntry(AssetName) is not {} entry)
		{
			return new byte[0];
		}

		using var stream = entry.Open();
		return Helpers.ReadToEnd(stream);
	}
}