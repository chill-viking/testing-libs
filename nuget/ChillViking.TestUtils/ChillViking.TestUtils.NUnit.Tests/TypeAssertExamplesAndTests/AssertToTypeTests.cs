using ChillViking.TestUtils.NUnit.Asserts;

namespace ChillViking.TestUtils.NUnit.Tests.TypeAssertExamplesAndTests;

public class OtherChild : Base
{
    public string Universe { get; set; } = "Universe";
}

public class AssertToTypeTests
{
    [Test]
    public void AssertToType_NotProperType_ThrowsException()
    {
        Base child = new OtherChild();

        var exception = Assert.Throws<AssertionException>(
            () => child.AssertToType(out Child _));

        Assert.Multiple(() =>
        {
            Assert.That(exception?.Message, Does.Contain("Failed to confirm 'child as Base' is <Child>"));
        });
    }
}
