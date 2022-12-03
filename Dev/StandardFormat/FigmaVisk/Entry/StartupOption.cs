using System;
using System.Diagnostics.CodeAnalysis;

namespace FigmaVisk
{
	public class StartupOption
	{
		public string? FileId { get; set; }
		public string? Token { get; set; }
		public string? OutputPath { get; set; }

		[MemberNotNull(nameof(FileId), nameof(Token), nameof(OutputPath))]
		public void CheckOption()
		{
			if (FileId is null || Token is null || OutputPath is null)
			{
				throw new InvalidOperationException(
					$"Usase:\n{Program.AppName}.exe " +
					$"{nameof(FileId)}=\"<figma file id>\" " +
					$"{nameof(Token)}=\"<figma access token>\"" +
					$"{nameof(OutputPath)}=\"<output package path>\"");
			}
		}
	}
}
