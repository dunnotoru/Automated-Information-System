using System.Globalization;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

internal class PriceModifierPercentValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        int number;
        if (!int.TryParse(value as string, out number))
            return new ValidationResult(false, "Invalid data");

        if(number < 0 || number > 100)
            return new ValidationResult(false, "Invalid range");

        return ValidationResult.ValidResult;
    }
}