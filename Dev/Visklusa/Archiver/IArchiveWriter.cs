using System;

namespace Visklusa.Archiver
{
	public interface IArchiveWriter : IDisposable
	{
		IAssetWriter GetAssetWriter(string filePath);
	}
}