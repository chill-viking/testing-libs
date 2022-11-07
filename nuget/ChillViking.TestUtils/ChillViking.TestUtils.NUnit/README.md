# ChillViking.TestUtils.NUnit

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=chill-viking-org_testing-libs-nunit&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=chill-viking-org_testing-libs-nunit)

Basic collection of extensions methods to use for testing with NUnit framework.

## Type Asserts

Following extension methods are available to use:

- [AssertToType](#asserttotype)

### AssertToType

Extension method created to do basic type validation and have access to the typed variable.

#### AssertToType Signature

```csharp
public static void AssertToType<TActual, TType>(this TActual actual, out TType result) where TType : TActual
```

#### AssertToType Usage

The following is a very simple case, but purely meant to highlight the primary purpose of this extension method.

##### Initial code for AssertToType example

```csharp
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
```

##### Test using AssertToType

```csharp
using namespace ChillViking.TestUtils.NUnit.Asserts;

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
```

## Collection Asserts

Following extension methods are available to use:

- [AssertCollection](#assertcollection)

### AssertCollection

Extension method created to do enumerate through a variable's elements and apply assert actions for each element.

:warn: This method is currently highly dependant on the order of the collection being tested.

#### AssertCollection Signature

```csharp
public static void AssertCollection<TActual>(this IEnumerable<TActual> items, params Action<TActual>[] assertions)
```

#### AssertCollection Usage

```csharp
using namespace ChillViking.TestUtils.NUnit.Asserts;

public class AssertCollectionExample
{
    [Test]
    public void AssertCollection()
    {
        var collection = new[] { "1", "2" };

        collection.AssertCollection(
            colValue => Assert.That(colValue, Is.EqualTo("1")),
            colValue => Assert.That(colValue, Is.EqualTo("2")));
    }
}
```
