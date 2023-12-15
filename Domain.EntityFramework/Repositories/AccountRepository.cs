using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public void Add(Account entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (AccountContext context = new AccountContext())
            {
                context.Accounts.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (AccountContext context = new AccountContext())
            {
                Account stored = context.Accounts.Single(o => o.Id == id);
                context.Accounts.Remove(stored);
                context.SaveChanges();
            }
        }

        public void Update(int id, Account entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (AccountContext context = new AccountContext())
            {
                Account stored = context.Accounts.Single(o => o.Id == id);

                stored = entity;
                stored.Id = id;
                
                context.Accounts.Update(entity);
                context.SaveChanges();
            }
        }

        public Account? GetById(int id)
        {
            using (AccountContext context = new AccountContext())
            {
                return context.Accounts.SingleOrDefault(_ => _.Id == id);
            }
        }

        public Account? GetByUsername(string username)
        {
            using (AccountContext context = new AccountContext())
            {
                return context.Accounts.SingleOrDefault(_ => _.Username == username);
            }
        }
    }
}
