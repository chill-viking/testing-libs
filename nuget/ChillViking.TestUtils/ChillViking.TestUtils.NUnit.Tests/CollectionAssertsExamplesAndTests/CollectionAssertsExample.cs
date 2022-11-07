using ChillViking.TestUtils.NUnit.Asserts;

namespace ChillViking.TestUtils.NUnit.Tests.CollectionAssertsExamplesAndTests;

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
