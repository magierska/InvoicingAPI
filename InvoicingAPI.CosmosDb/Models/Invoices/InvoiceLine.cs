namespace InvoicingAPI.CosmosDb.Models.Invoices;

public class InvoiceLine
{
    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
