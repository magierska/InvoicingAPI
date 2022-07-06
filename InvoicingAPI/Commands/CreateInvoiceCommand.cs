using InvoicingAPI.Contracts.Invoices;
using MediatR;

namespace InvoicingAPI.Commands;

public record CreateInvoiceCommand(Guid CustomerId, string? Description, IReadOnlyCollection<InvoiceLine> Lines) : IRequest<Guid>
{
}
