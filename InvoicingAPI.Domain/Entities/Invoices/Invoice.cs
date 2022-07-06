namespace InvoicingAPI.Domain.Entities.Invoices;

public class Invoice
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTimeOffset Date { get; set; }

    public string? Description { get; set; }

    public IReadOnlyCollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();

    public decimal TotalAmount => Lines.Sum(l => l.Amount);

    public InvoiceState State { get; set; }
}
