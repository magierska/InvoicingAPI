using InvoicingAPI.Contracts.Customers;
using MediatR;

namespace InvoicingAPI.Commands;

public record CreateCustomerCommand(string FirstName, string LastName, string? Address, IReadOnlyCollection<ContactDetail> ContactDetails) : IRequest<Guid>
{
}
