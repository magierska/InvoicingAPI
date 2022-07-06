using InvoicingAPI.Domain.Exceptions;
using Microsoft.Azure.Cosmos;
using System.Runtime.Serialization;

namespace InvoicingAPI.CosmosDb.Exceptions;

[Serializable]
public class CosmosRecordNotFoundException : RecordNotFoundException
{
    public CosmosRecordNotFoundException()
    {
    }

    public CosmosRecordNotFoundException(string? message) : base(message)
    {
    }

    public CosmosRecordNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public CosmosRecordNotFoundException(Guid id, Guid partitionKey, Container container, Exception? innerException = null) 
        : base ($"Record with id: '{id}' and partition key: '{partitionKey}' was not found in the container: '{container.Id}'.", innerException)
    {
    }

    protected CosmosRecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
