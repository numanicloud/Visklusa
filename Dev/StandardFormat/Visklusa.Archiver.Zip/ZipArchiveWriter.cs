﻿using System.IO;
using System.IO.Compression;
using Visklusa.Abstraction.Archiver;

namespace Visklusa.Archiver.Zip
{
	public class ZipArchiveWriter : IArchiveWriter
	{
		private readonly ZipArchive _zip;

		public ZipArchiveWriter(string packagePath)
		{
			var file = File.Create(packagePath);
			_zip = new ZipArchive(file, ZipArchiveMode.Create);
		}

		public IAssetWriter GetAssetWriter(string assetName)
		{
			return new ZipAssetWriter(_zip, assetName);
		}

		public void Dispose()
		{
			_zip.Dispose();
		}
	}
}
