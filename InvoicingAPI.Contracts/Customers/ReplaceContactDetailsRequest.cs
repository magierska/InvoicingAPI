using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Contracts.Customers;

public class ReplaceContactDetailsRequest
{
    [Required]
    public IReadOnlyCollection<ContactDetail> ContactDetails { get; set; } = null!;
}
