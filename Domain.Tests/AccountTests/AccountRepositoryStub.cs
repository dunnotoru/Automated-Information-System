using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Tests.AccountTests
{
    internal class AccountRepositoryStub : IAccountRepository
    {
        private readonly HashSet<Account> _accounts;

        public AccountRepositoryStub(HashSet<Account> accounts)
        {
            _accounts = accounts;
        }

        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public void Remove(Account account)
        {
            _accounts.Remove(account);
        }

        public Account? GetById(int id)
        {
            return _accounts.SingleOrDefault(_ => _.Id== id);
        }

        public Account? GetByUsername(string username)
        {
            return _accounts.SingleOrDefault(_ => _.Username == username);
        }

        public void Save()
        {
            
        }

        public void Update(Account account)
        {
            Account storedAcocunt = _accounts.Single(_ => _.Id == account.Id);
            _accounts.Remove(storedAcocunt);
            _accounts.Add(account);
        }
    }
}
