using System;

namespace Visklusa.Abstraction.Archiver;

public interface IArchiveWriter : IDisposable
{
	IAssetWriter GetAssetWriter(string assetName);
}