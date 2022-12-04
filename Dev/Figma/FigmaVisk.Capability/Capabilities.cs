
using Visklusa.Abstraction.Semantics;

namespace FigmaVisk.Capability;

public record Paint(Fill Fill, Stroke Stroke) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.Paint";
	public string Id => IdToRead;
}

public record RoundedRectangle(float LeftTop, float RightTop, float RightBottom, float LeftBottom) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.RoundedRectangle";
	public string Id => IdToRead;

	public RoundedRectangle() : this(0, 0, 0, 0)
	{
	}

	public RoundedRectangle(float radius)
		: this(radius, radius, radius, radius)
	{
	}
}

public record ImageRef(string Url) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.Url";
	public string Id => IdToRead;
}

public record Text(string Content, string FontFamily, int FontSize, Fill Fill) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.Text";
	public string Id => IdToRead;
}

public record AltPosition(float X, float Y) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.AltPosition";
	public string Id => IdToRead;
}

public record FigmaId(string NodeId, string Name) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.FigmaId";
	public string Id => IdToRead;
}

public record VerticalScroll() : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.VerticalScroll";
	public string Id => IdToRead;
}

public record VerticalList(float Spacing) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.VerticalList";
	public string Id => IdToRead;
}

public record Line : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.Line";
	public string Id => IdToRead;
}

public record Polygon(Vertex[] Vertices) : ISerializableCapability
{
	public static string IdToRead => "FigmaVisk.Polygon";
	public string Id => IdToRead;
}

public record Fill(float Red, float Green, float Blue, float Alpha)
{
	public static Fill Blank => new Fill(0, 0, 0, 0);
}

public record Stroke(float Red, float Green, float Blue, float Alpha, int Weight)
{
	public static Stroke Blank => new Stroke(0, 0, 0, 0, 0);
}

public record Vertex(float X, float Y);