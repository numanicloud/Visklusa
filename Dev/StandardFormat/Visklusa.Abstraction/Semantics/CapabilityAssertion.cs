using System.Text;

namespace Visklusa.Abstraction.Semantics;

public record CapabilityAssertion(string[] Assertions)
{
    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.AppendJoin(",", Assertions);
        return true;
    }
}