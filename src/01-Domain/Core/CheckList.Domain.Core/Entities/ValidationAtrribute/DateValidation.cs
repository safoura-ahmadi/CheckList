using System.ComponentModel.DataAnnotations;

namespace CheckList.Domain.Core.Entities.ValidationAtrribute;

[AttributeUsage(AttributeTargets.Property)]
public class DateValidation : ValidationAttribute
{
    public DateTime MinDate { get; set; }
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true; 
        }

        if (value is DateTime dateValue)
        {
            return dateValue >= MinDate;
        }
        return false;
    }
}
