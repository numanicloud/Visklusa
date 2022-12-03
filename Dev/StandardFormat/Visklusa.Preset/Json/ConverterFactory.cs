using Visklusa.Notation.Json;

namespace Visklusa.Preset.Json
{
	public class ConverterFactory
	{
		public IJsonCapabilityConverter Transform2Converter() =>
			new JsonCapabilityBase<Transform2>(Transform2.Id);

		public IJsonCapabilityConverter TextureConverter() =>
			new JsonCapabilityBase<Texture>(Texture.Id);

		public IJsonCapabilityConverter ColorDataConverter() =>
			new JsonCapabilityBase<ColorData>(ColorData.Id);

		public IJsonCapabilityConverter RoundedRectangleConverter() =>
			new JsonCapabilityBase<RoundedRectangle>(RoundedRectangle.Id);
	}
}
