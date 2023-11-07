using Domain.Models.Users;
using Domain.EntityFramework.Entities;
using Domain.RepositoryInterfaces.AccountRepository;
using Microsoft.EntityFrameworkCore;
using Domain.EntityFramework.Mappers;

namespace Domain.EntityFramework.Repositories.AccountRepositoryImpl
{
    public class AccountRepository : IAccountRepository
    {
        private string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<bool> AddAsync(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            AccountEntity? accountEntity;
            using (AccountContext db = new AccountContext(_connectionString))
            {
                accountEntity = await db.Accounts.FirstOrDefaultAsync(_ => _.Name == name);
            }

            if (accountEntity == null)
                throw new NullReferenceException();

            return AccountMapper.ToDomain(accountEntity);
        }

        public Task<bool> UpdateAsync(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }
    }
}
