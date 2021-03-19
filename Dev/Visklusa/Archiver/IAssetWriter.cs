using System;

namespace Visklusa.Archiver
{
	public interface IAssetWriter
	{
		string FilePath { get; }
		void Write(ReadOnlySpan<byte> data);
	}
}