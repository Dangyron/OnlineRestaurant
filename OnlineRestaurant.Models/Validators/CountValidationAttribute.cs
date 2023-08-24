using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models.Validators;

public class CountValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null or 0)
            return new ValidationResult("Count must be greater then 0!");

        if (value is int)
            return ValidationResult.Success;
        
        return new ValidationResult("Count must be a number!");
    }
}