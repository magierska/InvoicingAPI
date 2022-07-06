using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Contracts.Customers;

public class CreateCustomerRequest
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    public IReadOnlyCollection<ContactDetail> ContactDetails { get; set; } = new List<ContactDetail>();
}
