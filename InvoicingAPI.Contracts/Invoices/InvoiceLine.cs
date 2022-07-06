using InvoicingAPI.Contracts.Attributes;

namespace InvoicingAPI.Contracts.Invoices;

public class InvoiceLine
{
    [GreaterThanZero]
    public decimal Quantity { get; set; }

    [GreaterThanZero]
    public decimal? UnitPrice { get; set; }
}
