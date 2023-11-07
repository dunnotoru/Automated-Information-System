using Domain.Core.Users;
using Domain.RepositoryInterfaces;

namespace Domain.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IAccountRepository _accountRepository;

        public AuthenticationService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<Account> AuthenticateAsync(string name, string password)
        {
            
        }
    }
}
