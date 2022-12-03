﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Visklusa.Archiver;
using Visklusa.Notation;
using Visklusa.Variant;

namespace Visklusa
{
	public class VisklusaSaver : IDisposable
	{
		private readonly IFormat _format;
		private IArchiveWriter _writer;
		private readonly List<string> _assets = new();

		public VisklusaSaver(IFormat format)
		{
			_format = format;
			_writer = _format.GetPackageWriter();
		}

		public void AddAsset(byte[] assetData, string dstPath)
		{
			var asset = _writer.GetAssetWriter(dstPath);
			asset.Write(assetData);
			_assets.Add(dstPath);
		}

		public void AddLayout(Layout layout)
		{
			var serializer = _format.GetSerializer();
			var json = serializer.Serialize(layout);
			var file = _writer.GetAssetWriter(_format.LayoutFileName);
			file.Write(json);
		}

		public void Dispose()
		{
			AddAssetList();
			_writer.Dispose();
		}

		private void AddAssetList()
		{
			var json = JsonSerializer.Serialize(_assets.ToArray());
			var bytes = Encoding.UTF8.GetBytes(json);
			_writer.GetAssetWriter("assets.json").Write(bytes);
		}
	}
}
