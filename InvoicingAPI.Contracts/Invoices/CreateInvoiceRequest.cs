using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Contracts.Invoices;

public class CreateInvoiceRequest
{
    [Required]
    public Guid? CustomerId { get; set; }

    public string? Description { get; set; }

    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
}
