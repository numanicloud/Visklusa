using System;
using System.Linq;
using System.Text;

namespace Visklusa.Abstraction.Semantics;

public record Element(int Id, params ICapability[] Capabilities)
{
    public TCapability? GetCapability<TCapability>()
        where TCapability : ICapability
    {
        return Capabilities.OfType<TCapability>().FirstOrDefault();
    }

    public virtual bool Equals(Element? other)
    {
        return other is { } element
               && Id == element.Id
               && Capabilities.SequenceEqual(element.Capabilities);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Capabilities.GetDeepHashCode(3));
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        var join = string.Join(", ", Capabilities.Select(x => x.ToString()));
        builder.Append($"Id = {Id}, Capabilities = {join}");
        return true;
    }
}