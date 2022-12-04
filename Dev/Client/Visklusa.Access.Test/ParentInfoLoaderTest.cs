using Visklusa.Abstraction.Semantics;
using Visklusa.Preset;

namespace Visklusa.Access.Test;

public class ParentInfoLoaderTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ノード1つの場合にそれだけをルートとして返す()
    {
        var layout = new Layout(
            new CapabilityAssertion(FamilyShip.IdToRead),
            new Element(0));
        var loader = new ParentInfoLoader(layout);

        var actual = loader.GetElementRoots().ToArray();

        Assert.That(actual.Length, Is.EqualTo(1));
        Assert.That(actual[0].Self, Is.EqualTo(layout.Elements[0]));

        AssertionContext.OnObject(actual)
            .OnSequence(x => x,
                l0 => l0.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[0]))
                    .OnSequence(x => x.Children));
    }

    [Test]
    public void ノード1つが子ノードを3つ持っている場合の構造を計算できる()
    {
        var layout = new Layout(
            new CapabilityAssertion(FamilyShip.IdToRead),
            new Element(0),
            new Element(1, new FamilyShip(0)),
            new Element(2, new FamilyShip(0)),
            new Element(3, new FamilyShip(0)));
        var loader = new ParentInfoLoader(layout);

        var actual = loader.GetElementRoots().ToArray();

        AssertionContext.OnObject(actual)
            .OnSequence(x => x,
                l0 => l0.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[0]))
                    .OnSequence(x => x.Children,
                        l1 => l1.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[1]))
                            .OnSequence(x => x.Children),
                        l1 => l1.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[2]))
                            .OnSequence(x => x.Children),
                        l1 => l1.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[3]))
                            .OnSequence(x => x.Children)));
    }

    [Test]
    public void ノードが3階層の場合の構造を計算できる()
    {
        var layout = new Layout(
            new CapabilityAssertion(FamilyShip.IdToRead),
            new Element(0),
            new Element(1, new FamilyShip(0)),
            new Element(2, new FamilyShip(1)));
        var loader = new ParentInfoLoader(layout);

        var actual = loader.GetElementRoots().ToArray();

        AssertionContext.OnObject(actual)
            .OnSequence(x => x,
                l0 => l0.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[0]))
                    .OnSequence(x => x.Children,
                        l1 => l1.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[1]))
                            .OnSequence(x => x.Children,
                                l2 => l2.AssertThat(x => x.Self, Is.EqualTo(layout.Elements[2]))
                                    .OnSequence(x => x.Children))));
    }
}