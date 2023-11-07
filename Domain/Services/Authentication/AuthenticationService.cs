using Domain.Models.Users;
using Domain.Exceptions;
using Domain.RepositoryInterfaces.AccountRepository;

namespace Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IAccountRepository _accountRepository;
        private IPasswordHasher _passwordHasher;

        public AuthenticationService(IAccountRepository accountRepository, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> AuthenticateAsync(string name, string password)
        {
            Account storedAccount = await _accountRepository.GetByNameAsync(name);

            if(storedAccount == null)
                throw new AccountNotFoundException($"Account with name {name} doesn't exits.");

            bool verificationResult = await VerifyPasswordAsync(storedAccount.PasswordHash, password);
            
            if (verificationResult == false)
                throw new InvalidPasswordException("Password does not match.");

            return storedAccount;
        }

        private async Task<bool> VerifyPasswordAsync(string storedPasswordHash, string password)
        {
            string currentPasswordHash = await _passwordHasher.HashPasswordAsync(password);

            if (!String.Equals(storedPasswordHash, currentPasswordHash))
                return false;

            return true;
        }
    }
}
