using System.Runtime.Serialization;

namespace ChillViking.TestUtils.NUnit.Exceptions;

[Serializable]
public class FailedAssertException : Exception
{
    public FailedAssertException(string message) : base(message)
    {
    }

    protected FailedAssertException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
