using Domain.Models.Users;
using Domain.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.EntityFramework.Mappers;
using Domain.RepositoryInterfaces;
using System.Linq.Expressions;
using System.Net.Sockets;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Domain.EntityFramework.Repositories.AccountRepositoryImpl
{
    public class AccountRepository : IAccountRepository
    {
        private string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(Account account)
        {
            if(account == null) throw new ArgumentNullException(nameof(account));

            AccountEntity entity =  AccountMapper.ToEntity(account);

            using(AccountContext context = new AccountContext(_connectionString))
            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByIdAsync(int id)
        => await Get(_ => _.Id == id);

        public async Task<Account> GetByUsernameAsync(string username)
        => await Get(_ => _.Name == username);

        private async Task<Account> Get(Expression<Func<AccountEntity, bool>> expression)
        {
            AccountEntity? accountEntity;
            using (AccountContext db = new AccountContext(_connectionString))
            {
                accountEntity = await db.Accounts.FirstOrDefaultAsync(expression);
            }

            if (accountEntity == null)
                throw new NullReferenceException();

            return AccountMapper.ToDomain(accountEntity);
        }

        public Task UpdateAsync(int id, Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
