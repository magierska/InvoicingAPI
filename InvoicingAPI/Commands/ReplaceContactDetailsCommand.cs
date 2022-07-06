using InvoicingAPI.Contracts.Customers;
using MediatR;

namespace InvoicingAPI.Commands;

public record ReplaceContactDetailsCommand(Guid CustomerId, IReadOnlyCollection<ContactDetail> ContactDetails) : IRequest
{
}
