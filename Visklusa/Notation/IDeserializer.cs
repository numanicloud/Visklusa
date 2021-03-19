namespace Visklusa.Notation
{
	public interface IDeserializer
	{
		Layout Deserialize(byte[] layout);
	}
}