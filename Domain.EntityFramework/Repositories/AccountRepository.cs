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

        public void Remove(Account entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (AccountContext context = new AccountContext())
            {
                context.Accounts.Remove(entity);
                context.SaveChanges();
            }
        }

        public void Update(Account entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (AccountContext context = new AccountContext())
            {
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
