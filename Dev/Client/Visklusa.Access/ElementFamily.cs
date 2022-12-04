using Visklusa.Abstraction.Semantics;

namespace Visklusa.Access;

public abstract class ElementFamily : IEquatable<ElementFamily>
{
    public required string Name { get; init; }
    public required Element Self { get; init; }
    public ElementFamily? Parent { get; protected set; }
    public abstract IReadOnlyList<ElementFamily> Children { get; }

    public int Id => Self.Id;

    public bool Equals(ElementFamily? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Self.Equals(other.Self)
               && Equals(Parent?.Self, other.Parent?.Self)
               && Children.SequenceEqual(other.Children);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is ElementFamily other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Self, Parent, Children);
    }
}