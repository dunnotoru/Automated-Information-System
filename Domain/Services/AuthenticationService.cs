using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class AuthenticationService
    {
        private readonly IPasswordValidator _passwordValidator;
        private readonly IAccountRepository _accountRepository;

        public AuthenticationService(IAccountRepository accountRepository,
            IPasswordValidator passwordValidator)
        {
            ArgumentNullException.ThrowIfNull(nameof(accountRepository));
            ArgumentNullException.ThrowIfNull(nameof(passwordValidator));

            _accountRepository = accountRepository;
            _passwordValidator = passwordValidator;
        }

        public Account? Authenticate(string username, string password)
        {
            Account? storedAccount = _accountRepository.GetByUsername(username);
            if (storedAccount == null) return null;

            if (_passwordValidator.Validate(password, storedAccount.PasswordHash))
                return storedAccount;
            else
                return null;
        }
    }
}
