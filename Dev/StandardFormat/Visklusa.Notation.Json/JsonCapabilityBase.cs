using System.Text.Json;
using Visklusa.Abstraction.Semantics;

namespace Visklusa.Notation.Json;

internal sealed class JsonCapabilityBase<TCapability> : IJsonCapabilityConverter
	where TCapability : ISerializableCapability
{
	public string Id { get; } = TCapability.IdToRead;

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