using SpaInqTest.NUnit;

namespace Test.SpaInqTest.NUnit;

public class CollectionAssertsTests
{
    [Test]
    public void AssertCollection_Passes()
    {
        var collection = new[] { "1", "2" };

        collection.AssertCollection(
            colValue => Assert.That(colValue, Is.EqualTo("1")),
            colValue => Assert.That(colValue, Is.EqualTo("2")));
        Assert.Pass("No exceptions thrown");
    }
}
