using System;
using System.Globalization;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

internal class DateTimeValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (TimeOnly.TryParse(value as string, out _))
        {
            return ValidationResult.ValidResult;
        }
        else
        {
            return new ValidationResult(false, "неправильный формат");
        }
    }
}