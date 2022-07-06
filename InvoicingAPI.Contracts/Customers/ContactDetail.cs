using InvoicingAPI.Contracts.Attributes;
using InvoicingAPI.Domain.Entities.Customers;

namespace InvoicingAPI.Contracts.Customers;

[ContactDetailFormat]
public class ContactDetail
{
    public ContactDetailType? Type { get; set; }

    public string Value { get; set; } = null!;
}