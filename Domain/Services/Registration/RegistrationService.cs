using Domain.Exceptions;
using Domain.Models.Users;
using Domain.RepositoryInterfaces.AccountRepository;
using Domain.Services.Hash;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Domain.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;
        
        public RegistrationService(IAccountRepository accountRepository, IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterAsync(string name, string password, 
            string passwordConfrimation, Permission permission)
        {
            Account a = await _accountRepository.GetByNameAsync(name);
            if(a != null)
                throw new UsernameAlreadyExistException($"User with name {name} already exist.");

            if (!String.Equals(password,passwordConfrimation))
                throw new PasswordConfrimationDoesnotMatch("Password confrimation doesn't match.");

            string hash = await _passwordHasher.HashPasswordAsync(password);

            AddAccountDTO dto = new AddAccountDTO(name, hash, permission);
            
            await _accountRepository.AddAsync(dto);
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
