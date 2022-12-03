using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Visklusa.Abstraction.Semantics;

[UsedImplicitly]
public record CapabilityAssertion(params string[] Assertions)
{
    public virtual bool Equals(CapabilityAssertion? other)
    {
        return other is { } assertion
               && Assertions.SequenceEqual(assertion.Assertions);
    }

    public override int GetHashCode()
    {
        return Assertions.GetDeepHashCode(9);
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.AppendJoin(",", Assertions);
        return true;
    }
}