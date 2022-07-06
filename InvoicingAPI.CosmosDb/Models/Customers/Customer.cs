namespace InvoicingAPI.CosmosDb.Models.Customers;

public class Customer
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Address { get; set; }

    public IReadOnlyCollection<Domain.Entities.Customers.ContactDetail> ContactDetails { get; set; } = new List<Domain.Entities.Customers.ContactDetail>();

    public EntityType EntityType { get; set; }
}
