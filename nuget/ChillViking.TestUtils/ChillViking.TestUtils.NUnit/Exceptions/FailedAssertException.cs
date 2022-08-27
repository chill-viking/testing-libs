namespace ChillViking.TestUtils.NUnit.Exceptions;

public class FailedAssertException : Exception
{
    public FailedAssertException(string message) : base(message)
    {
    }
}