using Visklusa.Archiver;
using Visklusa.Notation;

namespace Visklusa.Variant
{
	public interface IFormat
	{
		string LayoutFileName { get; }
		IArchiveReader GetPackageReader();
		IArchiveWriter GetPackageWriter();
		IDeserializer GetDeserializer();
		ISerializer GetSerializer();
	}
}
