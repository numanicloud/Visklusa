using System;
using System.Linq;
using System.Text;

namespace Visklusa.Abstraction.Semantics;

public record Layout(CapabilityAssertion Assertion, params Element[] Elements)
{
    public virtual bool Equals(Layout? other)
    {
        return other is { } layout
               && Assertion == layout.Assertion
               && Elements.SequenceEqual(layout.Elements);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Assertion, Elements.GetDeepHashCode(3));
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append($"Assertion = {Assertion}, Elements = {Elements.GetStringToPrint()}");
        return true;
    }
}