using System.Runtime.Serialization;

namespace InvoicingAPI.Domain.Exceptions;

[Serializable]
public class RecordNotFoundException : Exception
{
    public RecordNotFoundException()
    {
    }

    public RecordNotFoundException(string? message) : base(message)
    {
    }

    public RecordNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected RecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
