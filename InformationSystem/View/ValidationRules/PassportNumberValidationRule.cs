using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

public class PassportNumberValidationRule : ValidationRule
{
    public int Length { get; set; }

    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        string? number = value as string;
        if (string.IsNullOrWhiteSpace(number))
        {
            return new ValidationResult(false, "string is empty");
        }
        
        if (number.Any(c => !char.IsDigit(c)))
        {
            return new ValidationResult(false, "Invalid format");
        }

        return ValidationResult.ValidResult;
    }
}