using System.Globalization;
using System.Windows.Controls;

namespace UI.View.ValidationRules;

internal class PassportSeriesValidationRule : ValidationRule
{
    private int _length;

    public int Length
    {
        get { return _length; }
        set { _length = value; }
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string number = value.ToString();
        if (number.Length != Length)
            return new ValidationResult(false, "Invalid length");

        foreach (char c in number)
        {
            if (!char.IsDigit(c))
                return new ValidationResult(false, "Invalid format");
        }

        int n = int.Parse(number);
        if (n <= 101 || n > 999999)
            return new ValidationResult(false, "Invalid data");

        return ValidationResult.ValidResult;
    }
}