using System;

namespace Visklusa.Abstraction.Archiver;

public interface IArchiveReader : IDisposable
{
	IAssetReader GetAsset(string assetName);
}