using Visklusa.Abstraction.Semantics;

namespace Visklusa.Access;

public abstract class ElementFamily
{
    public required string Name { get; init; }
    public required Element Self { get; init; }
    public ElementFamily? Parent { get; protected set; }
    public abstract IReadOnlyList<ElementFamily> Children { get; }

    public int Id => Self.Id;
}