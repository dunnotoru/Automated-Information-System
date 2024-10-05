using System;
using System.Globalization;
using System.Windows.Controls;

namespace UI.View.ValidationRules;

internal class DateOfBirthValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        DateTime dateTime = (DateTime)value;
        if (dateTime >= DateTime.Now)
            return new ValidationResult(false, "Invalid data");

        return ValidationResult.ValidResult;
    }
}