using Visklusa.Abstraction.Semantics;

namespace Visklusa.Abstraction.Notation;

public interface ISerializer
{
	byte[] Serialize(Layout layout);
}