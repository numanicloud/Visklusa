using System.Linq;
using System.Text;

namespace Visklusa.Abstraction.Semantics;

public record Element(int Id, ICapability[] Capabilities)
{
    public TCapability? GetCapability<TCapability>()
        where TCapability : ICapability
    {
        return Capabilities.OfType<TCapability>().FirstOrDefault();
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        var join = string.Join(", ", Capabilities.Select(x => x.ToString()));
        builder.Append($"Id = {Id}, Capabilities = {join}");
        return true;
    }
}