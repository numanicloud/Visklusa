namespace Visklusa.Abstraction.Notation
{
	public interface IDeserializer
	{
		Layout Deserialize(byte[] layout);
	}
}