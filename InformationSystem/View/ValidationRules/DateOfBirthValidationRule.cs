using System;
using System.Globalization;
using System.Windows.Controls;

namespace InformationSystem.View.ValidationRules;

internal class DateOfBirthValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        DateTime dateTime = value as DateTime? ?? default;

        if (dateTime >= DateTime.Now)
        {
            return new ValidationResult(false, "Invalid data");
        }

        return ValidationResult.ValidResult;
    }
}