using Visklusa.Abstraction.Archiver;
using Visklusa.Abstraction.Notation;

namespace Visklusa.Abstraction.Variant;

public interface IVisklusaVariant
{
	string LayoutFileName { get; }
	IArchiveReader GetPackageReader();
	IArchiveWriter GetPackageWriter();
	IDeserializer GetDeserializer();
	ISerializer GetSerializer();
}