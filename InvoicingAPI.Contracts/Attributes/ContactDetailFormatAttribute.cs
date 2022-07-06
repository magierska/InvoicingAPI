using InvoicingAPI.Contracts.Customers;
using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Contracts.Attributes;

public class ContactDetailFormatAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null || value is not ContactDetail)
        {
            return false;
        }

        var contactDetail = (ContactDetail)value;
        ValidationAttribute validation;
        switch (contactDetail.Type)
        {
            case Domain.Entities.Customers.ContactDetailType.Email:
                validation = new EmailAddressAttribute();
                break;
            case Domain.Entities.Customers.ContactDetailType.Mobile:
                validation = new PhoneAttribute();
                break;
            default:
                return false;
        }

        return validation.IsValid(contactDetail.Value);
    }
}
