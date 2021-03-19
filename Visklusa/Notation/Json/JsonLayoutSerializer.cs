using System;
using System.Text;
using System.Text.Json;

namespace Visklusa.Notation.Json
{
	internal class JsonLayoutSerializer : ISerializer, IDeserializer
	{
		private readonly JsonSerializerOptions _options;

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
