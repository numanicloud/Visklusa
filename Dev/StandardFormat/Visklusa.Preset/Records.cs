using System.Drawing;
using System.Numerics;
using Visklusa.Abstraction.Notation;

namespace Visklusa.Preset
{
	public record Transform2(Vector2 Position, Vector2 Scale, float Angle) : ICapability
	{
		public static readonly string Id = "Visklusa.Basic.Transform2";

		public string CapabilityId { get; } = Id;
	}

	public record Texture(string FilePath) : ICapability
	{
		public static readonly string Id = "Visklusa.Basic.Texture";

		public string CapabilityId { get; } = Id;
	}

	public record ColorData(Color Color) : ICapability
	{
		public static readonly string Id = "Visklusa.Basic.ColorData";

		public string CapabilityId { get; } = Id;
	}

	public record RoundedRectangle
		(float TopLeft, float TopRight, float BottomLeft, float BottomRight) : ICapability
	{
		public static readonly string Id = "Visklusa.Basic.RoundedRectangle";

		public string CapabilityId { get; } = Id;
	}
}
