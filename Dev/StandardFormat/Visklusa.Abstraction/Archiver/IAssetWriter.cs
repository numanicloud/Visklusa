using System;

namespace Visklusa.Abstraction.Archiver
{
	public interface IAssetWriter
	{
		string FilePath { get; }
		void Write(ReadOnlySpan<byte> data);
	}
}