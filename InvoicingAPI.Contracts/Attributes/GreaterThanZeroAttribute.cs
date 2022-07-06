using System.ComponentModel.DataAnnotations;

namespace InvoicingAPI.Contracts.Attributes;

public class GreaterThanZeroAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value != null && decimal.TryParse(value.ToString(), out decimal d) && d > 0;
    }
}
