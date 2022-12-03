using Visklusa.Abstraction.Semantics;

namespace Visklusa.Abstraction.Notation;

public interface IDeserializer
{
	Layout Deserialize(byte[] layout);
}