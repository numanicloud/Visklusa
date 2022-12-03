namespace Visklusa.Abstraction.Archiver
{
	public interface IAssetReader
	{
		string FilePath { get; }
		byte[] Read();
	}
}