﻿using System;
using System.IO;
using System.IO.Compression;

namespace Visklusa.Archiver.Zip
{
	internal class ZipArchiveWriter : IArchiveWriter
	{
		private readonly FileStream _file;
		private readonly ZipArchive _zip;

		public ZipArchiveWriter(string packagePath)
		{
			_file = File.Create(packagePath);
			_zip = new ZipArchive(_file, ZipArchiveMode.Create);
		}

		public IAssetWriter GetDocumentWriter()
		{
			throw new NotImplementedException();
		}

		public IAssetWriter GetAssetWriter(string filePath)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_file.Dispose();
			_zip.Dispose();
		}
	}
}
