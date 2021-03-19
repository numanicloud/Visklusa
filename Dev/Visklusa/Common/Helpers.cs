using System.Collections.Generic;
using System.IO;

namespace Visklusa.Common
{
	public static class Helpers
	{
		public static byte[] ReadToEnd(Stream stream, int bufferSize = 1024)
		{
			var bytes = new List<byte>();
			var buffer = new byte[bufferSize];
			while (true)
			{
				var read = stream.Read(buffer);
				bytes.AddRange(buffer[..read]);
				if (read < bufferSize)
				{
					break;
				}
			}

			return bytes.ToArray();
		}
	}
}
