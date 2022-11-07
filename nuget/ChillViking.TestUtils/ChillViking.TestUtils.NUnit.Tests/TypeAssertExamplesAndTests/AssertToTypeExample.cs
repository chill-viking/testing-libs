using ChillViking.TestUtils.NUnit.Asserts;
using Moq;

namespace ChillViking.TestUtils.NUnit.Tests.TypeAssertExamplesAndTests;

// any changes here should be synced with README.md for project.
public class Base
{
    public string Hello { get; set; } = "Hello";
}

public class Child : Base
{
    public string World { get; set; } = "World";
}

public interface IBaseProvider
{
    Base GetBase();
}

public class ClassWithMethod
{
    private readonly IBaseProvider _baseProvider;

    public ClassWithMethod(IBaseProvider baseProvider)
    {
        _baseProvider = baseProvider;
    }

    public Base GetBase() => _baseProvider.GetBase();
}

public class ClassWithMethodTests
{
    private readonly Mock<IBaseProvider> _baseProviderMock = new();
    private ClassWithMethod _testClass = null!;

    [SetUp]
    public void Init()
    {
        _testClass = new ClassWithMethod(_baseProviderMock.Object);
    }

    [Test]
    public void GetBase_ReturnsExpected()
    {
        var mockValue = new Child
        {
            Hello = "hi",
            World = "mars",
        };
        _baseProviderMock.Setup(m => m.GetBase())
            .Returns(mockValue);

        var result = _testClass.GetBase();

        _baseProviderMock.Verify(m => m.GetBase());
        Assert.That(result.Hello, Is.EqualTo("hi"));
        // unable to assert `World` property for `result`...
        result.AssertToType(out Child actual);
        // now `World` property is available on `actual` variable and asserted the type of `result` is `Child`
        Assert.That(actual.World, Is.EqualTo("mars"));
    }
}
