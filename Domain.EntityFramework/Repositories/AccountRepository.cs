using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {

        }

        public void Add(Account entity)
        {
            
            _context.Accounts.Add(entity);
        }

        public void Remove(Account entity)
        {
            _context.Accounts.Remove(entity);
        }

        public void Update(Account entity)
        {
            _context.Accounts.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Account? GetById(int id)
        {
            return _context.Accounts.SingleOrDefault(_ => _.Id == id);
        }

        public Account? GetByUsername(string username)
        {
            return _context.Accounts.SingleOrDefault(_ => _.Username == username);
        }
    }
}
