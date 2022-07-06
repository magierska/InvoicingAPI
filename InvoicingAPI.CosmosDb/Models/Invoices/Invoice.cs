using InvoicingAPI.Domain.Entities.Invoices;

namespace InvoicingAPI.CosmosDb.Models.Invoices;

public class Invoice
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTimeOffset Date { get; set; }

    public string? Description { get; set; }

    public IReadOnlyCollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();

    public InvoiceState State { get; set; }

    public EntityType EntityType { get; set; }
}
