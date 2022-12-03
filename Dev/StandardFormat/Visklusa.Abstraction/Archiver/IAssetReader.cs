namespace Visklusa.Abstraction.Archiver;

public interface IAssetReader
{
	string AssetName { get; }
	byte[] Read();
}