using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvoicingAPI.CosmosDb.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum EntityType
{
    Customer,
    Invoice
}
