using System;
using System.Text;
using System.Text.Json;
using Visklusa.Abstraction.Notation;

namespace Visklusa.Notation.Json
{
	public class JsonLayoutSerializer : ISerializer, IDeserializer
	{
		private JsonSerializerOptions _options;

		public JsonSerializerOptions Options
		{
			get => _options;
			set => _options = value;
		}

		public JsonLayoutSerializer(JsonCapabilityRepository repository)
		{
			_options = new JsonSerializerOptions();
			_options.Converters.Add(new ElementJsonConverter(repository));
		}

		public byte[] Serialize(Layout layout)
		{
			var json = JsonSerializer.Serialize(layout, _options);
			return Encoding.UTF8.GetBytes(json);
		}

		public Layout Deserialize(byte[] layout)
		{
			var text = Encoding.UTF8.GetString(layout);
			return JsonSerializer.Deserialize<Layout>(text, _options)
			       ?? throw new Exception();
		}
	}
}
