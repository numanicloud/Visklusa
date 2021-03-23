using System.Text.Json;
using Visklusa.Abstraction.Notation;

namespace Visklusa.Notation.Json
{
	public class JsonCapabilityBase<TCapability> : IJsonCapabilityConverter
		where TCapability : ICapability
	{
		public string Id { get; }

		public JsonCapabilityBase(string id)
		{
			Id = id;
		}

		public ICapability? Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
		{
			return JsonSerializer.Deserialize<TCapability>(ref reader, options);
		}

		public void Write(Utf8JsonWriter writer, ICapability value, JsonSerializerOptions options)
		{
			var typed = (TCapability) value;
			JsonSerializer.Serialize(writer, typed, options);
		}
	}
}
