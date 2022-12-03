using System.Text;
using System.Text.Json;
using Visklusa.Abstraction.Semantics;
using Visklusa.Notation.Json;
using Visklusa.Preset;

namespace Visklusa.JsonZip.Test;

public class VariantTest
{
    private Layout SampleLayout;
    private string SampleLayoutJson;
    
    [SetUp]
    public void Setup()
    {
        SampleLayout = new Layout(
            new CapabilityAssertion(BoundingBox.IdToRead, ZOffset.IdToRead),
            new Element(0, new BoundingBox(0, 0, 1, 1)));
        
        SampleLayoutJson = @"
{
    ""Assertion"": {
        ""Assertions"": [
            ""Visk.BoundingBox"",
            ""Visk.ZOffset""
        ]
    },
    ""Elements"": [
        {
            ""Id"": 0,
            ""Visk.BoundingBox"": {
                ""X"": 0,
                ""Y"": 0,
                ""Width"": 1,
                ""Height"": 1
            }
        }
    ]
}".Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
    }

    [Test]
    public void シリアライズのオプションをカスタマイズできる()
    {
        var variant = new JsonZipVariant("package", new JsonCapabilityRepository());
        
        variant.SetOptionModifier(option => new JsonSerializerOptions(option)
        {
            MaxDepth = 1,
        });

        if (variant.GetDeserializer() is not JsonLayoutSerializer deserializer)
            throw new Exception();
        
        Assert.That(deserializer.Options.MaxDepth, Is.EqualTo(1));
    }

    [Test]
    public void layoutファイルをシリアライズできる()
    {
        var repo = new JsonCapabilityRepository();
        repo.Register<BoundingBox>();
        repo.Register<ZOffset>();

        var converter = new JsonZipVariant("package", repo);
        var data = converter.GetSerializer().Serialize(SampleLayout);
        var text = Encoding.UTF8.GetString(data);

        Assert.That(text, Is.EqualTo(SampleLayoutJson));
    }

    [Test]
    public void layoutファイルをデシリアライズできる()
    {
        var repo = new JsonCapabilityRepository();
        repo.Register<BoundingBox>();
        repo.Register<ZOffset>();

        var converter = new JsonZipVariant("package", repo);
        var data = Encoding.UTF8.GetBytes(SampleLayoutJson);
        var layout = converter.GetDeserializer().Deserialize(data);
        
        Assert.That(layout, Is.EqualTo(SampleLayout));
    }
}