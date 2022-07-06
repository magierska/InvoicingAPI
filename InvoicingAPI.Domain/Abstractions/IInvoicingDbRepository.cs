using InvoicingAPI.Domain.Entities.Customers;
using InvoicingAPI.Domain.Entities.Invoices;

namespace InvoicingAPI.Domain.Abstractions;

public interface IInvoicingDbRepository
{
    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task ReplaceContactDetailsAsync(Guid customerId, IEnumerable<ContactDetail> contactDetails, CancellationToken cancellationToken = default);
    Task<Invoice> GetInvoiceAsync(Guid invoiceId, Guid customerId, CancellationToken cancellationToken = default);
    Task CreateInvoiceAsync(Invoice invoice, CancellationToken cancellationToken = default);
    Task UpdateStateAsync(Guid invoiceId, Guid customerId, InvoiceState state, CancellationToken cancellationToken = default);
}
