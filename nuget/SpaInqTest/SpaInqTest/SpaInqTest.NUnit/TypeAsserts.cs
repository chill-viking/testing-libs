using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace SpaInqTest.NUnit;

public static class TypeAsserts
{
    internal static string GetTypeName(Type type)
    {
        // may need to include other variations in future.
        if (!type.IsGenericType)
            return type.Name;

        var genericParameters = type.GenericTypeArguments.Select(GetTypeName).ToArray();
        return
            $"{type.Name.Replace($"`{genericParameters.Length}", string.Empty)}<{string.Join(", ", genericParameters)}>";
    }

    /// <summary>
    /// Assert actual value derived from <typeparamref name="TActual"/> is actually <typeparamref name="TType"/>
    /// and set it into <paramref name="result"/>
    /// </summary>
    /// <param name="actual">Value to assert</param>
    /// <param name="result">will be assigned <paramref name="actual"/> as <typeparamref name="TType"></typeparamref></param>
    /// <param name="actualName"></param>
    /// <typeparam name="TType">Derived type of <typeparamref name="TActual"/></typeparam>
    /// <typeparam name="TActual">Current type of <paramref name="actual"/></typeparam>
    /// <returns>true when assert is successful</returns>
    ///
    public static void AssertToType<TActual, TType>(
        this TActual actual,
        out TType result,
        [CallerArgumentExpression("actual")] string? actualName = null) where TType : TActual
    {
        try
        {
            Assert.That(actual, Is.Not.Null.And.TypeOf(typeof(TType)));
            result = (TType) actual!;
        }
        catch (Exception e)
        {
            throw new AssertionException(
                string.Join(
                    " ",
                    $"Failed to confirm '{actualName} as {GetTypeName(typeof(TActual))}'",
                    $"is <{GetTypeName(typeof(TType))}>.{Environment.NewLine}{e.Message}"),
                e);
        }
    }
}
