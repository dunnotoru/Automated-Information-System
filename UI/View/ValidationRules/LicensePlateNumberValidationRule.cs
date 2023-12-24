using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace UI.View.ValidationRules
{
    internal class LicensePlateNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex("^[A-Za-zА-Яа-я]{1}\\d{3}[A-Za-zА-Яа-я]{2}\\d{1,3}$");
            if (string.IsNullOrEmpty(value as string))
                return new ValidationResult(false, "the string is empty");

            if (regex.IsMatch(value as string))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, "the string does not match");
        }
    }
}
