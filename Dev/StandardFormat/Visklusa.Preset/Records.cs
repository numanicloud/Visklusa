using Visklusa.Abstraction.Notation;

namespace Visklusa.Preset;

public record struct Vector2(float X, float Y);

public record BoundingBox(Vector2 Position, Vector2 Size) : IStaticCapability
{
    public static string CapabilityId => "Visk.BoundingBox";
}

public record FamilyShip(int Id) : IStaticCapability
{
    public static string CapabilityId => "Visk.FamilyShip";
}

public record ZOffset(int Z) : IStaticCapability
{
    public static string CapabilityId => "Visk.ZOffset";
}

public record Image(string AssetName) : IStaticCapability
{
    public static string CapabilityId => "Visk.Image";
}