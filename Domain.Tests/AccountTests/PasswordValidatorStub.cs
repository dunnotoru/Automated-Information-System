using Domain.UseCases.AccountUseCases;

namespace Domain.Tests.AccountTests
{
    internal class PasswordValidatorStub : IPasswordValidator
    {
        private IPasswordHasher _hasher;

        public PasswordValidatorStub(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }

        public bool Validate(string value, string storedValue)
        {
            string valueHash = _hasher.CalcHash(value);
            return valueHash == storedValue;
        }
    }
}
