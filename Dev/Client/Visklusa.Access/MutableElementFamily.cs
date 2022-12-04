using Visklusa.Preset;

namespace Visklusa.Access;

internal sealed class MutableElementFamily : ElementFamily
{
    public List<ElementFamily> ChildList { get; } = new();
    public override IReadOnlyList<ElementFamily> Children => ChildList;

    public void LoadFamilyShip(Dictionary<int, MutableElementFamily> allElement)
    {
        if (Self.GetCapability<FamilyShip>() is not {} familyShip) return;

        var parent = allElement[familyShip.ParentId];
        Parent = parent;
        parent.ChildList.Add(this);
    }
}