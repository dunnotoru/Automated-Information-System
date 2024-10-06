using System.Globalization;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string? text = value as string;

        if (string.IsNullOrWhiteSpace(text))
            return new ValidationResult(false, "string is empty");

        return ValidationResult.ValidResult;
    }
}