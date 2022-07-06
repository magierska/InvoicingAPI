using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvoicingAPI.Domain.Entities.Customers;

[JsonConverter(typeof(StringEnumConverter))]
public enum ContactDetailType
{
    Email,
    Mobile
}
