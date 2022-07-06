namespace InvoicingAPI.Domain.Entities.Customers;

public class Customer
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Address { get; set; }

    public IReadOnlyCollection<ContactDetail> ContactDetails { get; set; } = new List<ContactDetail>();
}
