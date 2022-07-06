using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InvoicingAPI.Domain.Entities.Invoices;

[JsonConverter(typeof(StringEnumConverter))]
public enum InvoiceState
{
    Draft,
    Issued,
    Paid,
    Deleted,
    Cancelled
}
