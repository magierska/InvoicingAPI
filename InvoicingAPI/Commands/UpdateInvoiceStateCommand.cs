using InvoicingAPI.Domain.Entities.Invoices;
using MediatR;

namespace InvoicingAPI.Commands;

public record UpdateInvoiceStateCommand(Guid InvoiceId, Guid CustomerId, InvoiceState State) : IRequest
{
}
