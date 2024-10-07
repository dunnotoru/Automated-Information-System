using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

internal class PassportSeriesValidationRule : ValidationRule
{
    public int Length { get; set; }

    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        string? number = value as string;

        if (string.IsNullOrWhiteSpace(number))
        {
            return new ValidationResult(false, "empty string");
        }

        if (number.Length != Length)
        {
            return new ValidationResult(false, "Invalid length");
        }

        if (number.Any(c => !char.IsDigit(c)))
        {
            return new ValidationResult(false, "Invalid format");
        }

        if (int.Parse(number) is <= 101 or > 999999)
        {
            return new ValidationResult(false, "Invalid data");
        }

        return ValidationResult.ValidResult;
    }
}