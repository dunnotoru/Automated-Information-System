using System.Globalization;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace(value as string))
        {
            return new ValidationResult(false, "string is empty");
        }

        return ValidationResult.ValidResult;
    }
}