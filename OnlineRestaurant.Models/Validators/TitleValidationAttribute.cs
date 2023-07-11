using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models.Validators;

public class TitleValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return new ValidationResult("Title is required");
        
        string title = Convert.ToString(value)!;
        
        if (title.Length < 2)
            return new ValidationResult("Title length should be more than 2");
        
        return ValidationResult.Success;
    }
}