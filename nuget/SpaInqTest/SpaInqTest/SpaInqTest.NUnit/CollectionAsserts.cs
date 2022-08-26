using NUnit.Framework;

namespace SpaInqTest.NUnit;

public static class CollectionAsserts
{
    internal static IEnumerable<Exception?> PerformCollectionActions<TActual>(
        IEnumerable<Action<TActual>> assertions,
        TActual[] actualElements)
    {
        return assertions.Select((ass, i) =>
        {
            try
            {
                ass.Invoke(actualElements[i]);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        });
    }

    /// <summary>
    /// Assert a collection of elements using passed in assertions for each element.
    /// Will confirm length of <paramref name="items"/> matches <paramref name="assertions"/> and then loop through each item.
    /// </summary>
    /// <param name="items">Collection to assert</param>
    /// <param name="assertions">Actions to apply to each element in <paramref name="items"/>, use Assert to throw an exception in action</param>
    /// <typeparam name="TActual"></typeparam>
    /// <exception cref="AssertionException">Thrown when assertions fail</exception>
    public static void AssertCollection<TActual>(
        this IEnumerable<TActual> items,
        params Action<TActual>[] assertions)
    {
        try
        {
            var actualElements = items as TActual[] ?? items.ToArray();
            Assert.That(actualElements, Is.Not.Null);
            var exceptions = PerformCollectionActions(assertions, actualElements)
                .Where(e => e != null)
                .ToArray();

            if (!exceptions.Any())
                return;

            var combinedMessages = string.Join(
                Environment.NewLine,
                exceptions.Select(e => e?.Message));
            throw new Exception(combinedMessages);
        }
        catch (Exception e)
        {
            throw new AssertionException(
                string.Join(
                    Environment.NewLine,
                    $"Failed to assert collection of <{TypeAsserts.GetTypeName(typeof(TActual))}>",
                    $"Exception: {e.Message}",
                    e.InnerException != null ? $"Inner Exception: {e.InnerException.Message}" : string.Empty),
                e);
        }
    }
}
