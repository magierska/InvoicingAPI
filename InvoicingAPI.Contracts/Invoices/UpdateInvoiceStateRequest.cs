using InvoicingAPI.Domain.Entities.Invoices;
using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Contracts.Invoices;

public class UpdateInvoiceStateRequest
{
    [Required]
    public Guid? CustomerId { get; set; }

    [Required]
    public InvoiceState? State { get; set; }
}
