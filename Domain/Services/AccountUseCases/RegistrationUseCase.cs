using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services.AccountUseCases
{
    public class RegistrationUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegistrationUseCase(IAccountRepository accountRepository,
            IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }

        public bool Register(string username, string password,
            bool read = false, bool write = false, bool edit = false, bool delete = false)
        {
            Account? storedAccount = _accountRepository.GetByUsername(username);
            if (storedAccount == null) return false;

            string passwordHash = _passwordHasher.CalcHash(password);
            Account newAccount = new Account()
            {
                Username = username,
                PasswordHash = passwordHash,
                Read = read,
                Write = write,
                Edit = edit,
                Delete = delete
            };

            _accountRepository.Add(newAccount);

            return true;
        }
    }
}
