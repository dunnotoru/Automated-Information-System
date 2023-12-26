using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace UI.View.ValidationRules
{
    internal class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = value as string;
            if (password == null)
                return new ValidationResult(false, "empty string");

            if(password.Length < 6)
                return new ValidationResult(false, "too short");

            if (!password.Any(char.IsDigit) || !password.Any(char.IsLetter) || !password.Any(IsSpecialCharacter))
                return new ValidationResult(false, "too simple password");

            if (password.Contains("123") || password.Contains("qwe"))
                return new ValidationResult(false, "too simple password");

            if (AreAllCharactersSame(password))
                return new ValidationResult(false, "all characters are the same");

            return ValidationResult.ValidResult;
        }

        private bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c);
        }

        private bool AreAllCharactersSame(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            char firstChar = input[0];

            foreach (char c in input)
            {
                if (c != firstChar)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
