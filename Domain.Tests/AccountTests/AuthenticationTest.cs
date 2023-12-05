using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.UseCases.AccountUseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.AccountTests
{
    [TestClass]
    public class AuthenticationTest
    {
        [TestMethod]
        public void Authenticate_ValidCredentials_Success()
        {
            HashSet<Account> accounts = new HashSet<Account>();
            IPasswordHasher hasher = new PasswordHasherStub();
            IPasswordValidator validator = new PasswordValidatorStub(hasher);
            IAccountRepository repository = new AccountRepositoryStub(accounts);
            AuthenticationUseCase authentication 
                = new AuthenticationUseCase(repository,validator);
            
            string username = "JonDoe";
            string password = "SecretPassword";

            Account account = new Account()
            {
                Username = username,
                PasswordHash = hasher.CalcHash(password),
            };

            repository.Add(account);

            bool result = authentication.Authenticate(username, password) != null;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Authenticate_UnexistingUsername_Failure()
        {
            HashSet<Account> accounts = new HashSet<Account>();
            IPasswordHasher hasher = new PasswordHasherStub();
            IPasswordValidator validator = new PasswordValidatorStub(hasher);
            IAccountRepository repository = new AccountRepositoryStub(accounts);
            AuthenticationUseCase authentication
                = new AuthenticationUseCase(repository, validator);

            string username = "JonDoe";
            string password = "SecretPassword";

            string wrongUsername = "Ivan";

            Account account = new Account()
            {
                Username = username,
                PasswordHash = hasher.CalcHash(password),
            };

            repository.Add(account);

            bool result = authentication.Authenticate(wrongUsername, password) != null;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Authenticate_WrongPassword_Failure()
        {
            HashSet<Account> accounts = new HashSet<Account>();
            IPasswordHasher hasher = new PasswordHasherStub();
            IPasswordValidator validator = new PasswordValidatorStub(hasher);
            IAccountRepository repository = new AccountRepositoryStub(accounts);
            AuthenticationUseCase authentication
                = new AuthenticationUseCase(repository, validator);

            string username = "JonDoe";
            string password = "SecretPassword";

            string wrongPassword = "wrongPassword";

            Account account = new Account()
            {
                Username = username,
                PasswordHash = hasher.CalcHash(password),
            };

            repository.Add(account);

            bool result = authentication.Authenticate(username, wrongPassword) != null;
            
            Assert.IsFalse(result);
        }
    }
}
