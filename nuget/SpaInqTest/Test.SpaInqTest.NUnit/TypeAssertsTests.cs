using SpaInqTest.NUnit;

namespace Test.SpaInqTest.NUnit;

public class Base
{
    public string Hello { get; set; }
}

public class Child : Base
{
    public string World { get; set; }
}

public class TypeAssertsTests
{
    [Test]
    public void AssertToType_ReturnsType()
    {
        Base value = new Child
        {
            Hello = "hi",
            World = "mars",
        };

        value.AssertToType(out Child actual);

        Assert.That(actual, Is.TypeOf<Child>());
        Assert.That(actual.Hello, Is.EqualTo("hi"));
        Assert.That(actual.World, Is.EqualTo("mars"));
    }
}
