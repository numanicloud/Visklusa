using System.Collections.Generic;
using System.Linq;
using Visklusa.Abstraction.Semantics;
using Visklusa.Notation.Json;

namespace FigmaVisk.Entry;

internal class ConvertContext
{
    public JsonCapabilityRepository Repository { get; } = new();
    private readonly HashSet<string> _capabilityIds = new();

    public ConvertContext Register<T>() where T : ISerializableCapability
    {
        Repository.Register<T>();
        _capabilityIds.Add(T.IdToRead);
        return this;
    }

    public CapabilityAssertion GetAssertion() => new(_capabilityIds.ToArray());
}