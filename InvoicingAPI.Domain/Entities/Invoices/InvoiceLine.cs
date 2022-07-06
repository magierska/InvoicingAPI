namespace InvoicingAPI.Domain.Entities.Invoices;

public class InvoiceLine
{
    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Amount => Quantity * UnitPrice;
}
