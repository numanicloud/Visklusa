using System;

namespace Visklusa.Archiver
{
	public interface IArchiveReader : IDisposable
	{
		IAssetReader GetAsset(string filePath);
	}
}