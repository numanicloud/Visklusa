using Visklusa.Abstraction.Semantics;

namespace Visklusa.Access;

public class ParentInfoLoader
{
    private readonly Dictionary<int, Element> _elements;
    private IEnumerable<ElementFamily>? _elementRootsCache;

    public ParentInfoLoader(Layout layout)
    {
        _elements = layout.Elements
            .ToDictionary(x => x.Id, x => x);
    }

    public IEnumerable<ElementFamily> GetElementRoots()
    {
        if (_elementRootsCache is not null) return _elementRootsCache;

        var flat = GetFlatFamily().ToArray();
        var dic = flat.ToDictionary(x => x.Id, x => x);
        foreach (var node in flat)
        {
            node.LoadFamilyShip(dic);
        }

        return _elementRootsCache ??= flat.Where(x => x.Parent is null);
    }

    private IEnumerable<MutableElementFamily> GetFlatFamily()
    {
        foreach (var (key, element) in _elements)
        {
            yield return new MutableElementFamily
            {
                Name = key.ToString(),
                Self = element
            };
        }
    }
}