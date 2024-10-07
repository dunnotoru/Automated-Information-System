using System.Globalization;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

internal class PriceModifierPercentValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (!int.TryParse(value as string, out int number))
        {
            return new ValidationResult(false, "Invalid data");
        }

        if (number is < 0 or > 100)
        {
            return new ValidationResult(false, "Invalid range");
        }

        return ValidationResult.ValidResult;
    }
}