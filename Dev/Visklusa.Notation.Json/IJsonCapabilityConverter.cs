using System.Text.Json;
using Visklusa.Abstraction.Notation;

namespace Visklusa.Notation.Json
{
	public interface IJsonCapabilityConverter
	{
		string Id { get; }
		ICapability? Read(ref Utf8JsonReader reader, JsonSerializerOptions options);
		void Write(Utf8JsonWriter writer, ICapability value, JsonSerializerOptions options);
	}
}
