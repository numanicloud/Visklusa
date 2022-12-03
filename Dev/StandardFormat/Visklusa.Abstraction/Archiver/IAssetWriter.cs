using System;

namespace Visklusa.Abstraction.Archiver;

public interface IAssetWriter
{
	string AssetName { get; }
	void Write(ReadOnlySpan<byte> data);
}