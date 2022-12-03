using Visklusa.Abstraction.Semantics;

namespace Visklusa.Preset;

public record BoundingBox(int X, int Y, int Width, int Height) : ISerializableCapability
{
    public static string IdToRead => "Visk.BoundingBox";
    public string Id => IdToRead;
}

public record FamilyShip(int ParentId) : ISerializableCapability
{
    public static string IdToRead => "Visk.FamilyShip";
    public string Id => IdToRead;
}

public record ZOffset(int Z) : ISerializableCapability
{
    public static string IdToRead => "Visk.ZOffset";
    public string Id => IdToRead;
}

public record Image(string AssetName) : ISerializableCapability
{
    public static string IdToRead => "Visk.Image";
    public string Id => IdToRead;
}