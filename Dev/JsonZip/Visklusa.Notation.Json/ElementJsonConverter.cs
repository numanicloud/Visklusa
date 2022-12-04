using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Visklusa.Abstraction.Semantics;

namespace Visklusa.Notation.Json;

internal class ElementJsonConverter : JsonConverter<Element>
{
	private readonly JsonCapabilityRepository _repository;

	public ElementJsonConverter(JsonCapabilityRepository repository)
	{
		_repository = repository;
	}

	public override Element Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		AssertToken(ref reader, JsonTokenType.StartObject);

		reader.Read();
		var elementId = TryReadIntProperty(ref reader, nameof(Element.Id));
			
		var capabilites = new List<ICapability>();

		reader.Read();
		while (true)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.GetString() is not {} capabilityId)
				{
					throw new JsonException();
				}

				reader.Read();
				var capability = _repository.Get(capabilityId)?.Read(ref reader, options);
				if (capability is not null)
				{
					capabilites.Add(capability);
				}

				reader.Read();
				continue;
			}

			if (reader.TokenType == JsonTokenType.EndObject)
			{
				break;
			}
		}

		return new Element(elementId, capabilites.ToArray());
	}

	public override void Write(Utf8JsonWriter writer, Element value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
			
		writer.WriteNumber(nameof(Element.Id), value.Id);
		foreach (var capability in value.Capabilities)
		{
			writer.WritePropertyName(capability.Id);
			if (_repository.Get(capability.Id) is {} converter)
			{
				converter.Write(writer, capability, options);
			}
			else
			{
				writer.WriteRawValue("\"Error: Capability not found.\"");
			}
		}

		writer.WriteEndObject();
	}

	private void AssertToken(ref Utf8JsonReader reader, JsonTokenType tokenType)
	{
		if (reader.TokenType != tokenType)
		{
			throw new JsonException();
		}
	}

	private int TryReadIntProperty(ref Utf8JsonReader reader, string propertyName)
	{
		if (reader.TokenType != JsonTokenType.PropertyName)
		{
			throw new JsonException();
		}

		if (reader.GetString() != propertyName)
		{
			throw new JsonException();
		}

		reader.Read();
		if (reader.TokenType != JsonTokenType.Number)
		{
			throw new JsonException();
		}

		return reader.GetInt32();
	}
}