namespace Visklusa.Archiver
{
	public interface IAssetReader
	{
		string FilePath { get; }
		byte[] Read();
	}
}