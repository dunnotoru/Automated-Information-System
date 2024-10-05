using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IDbContextFactory<AccountContext> _factory;

    public AccountRepository(IDbContextFactory<AccountContext> factory)
    {
        _factory = factory;
    }

    public int Create(Account entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (AccountContext context = _factory.CreateDbContext())
        {
            context.Accounts.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Remove(int id)
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            Account stored = context.Accounts.First(o => o.Id == id);
            context.Accounts.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, Account entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (AccountContext context = _factory.CreateDbContext())
        {
            Account stored = context.Accounts.First(o => o.Id == id);

            stored.Read = entity.Read;
            stored.Write = entity.Write;
            stored.Edit = entity.Edit;
            stored.Delete = entity.Delete;
                
            context.SaveChanges();
        }
    }

    public Account GetById(int id)
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            return context.Accounts.First(_ => _.Id == id);
        }
    }

    public Account GetByUsername(string username)
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            return context.Accounts.First(_ => _.Username == username);
        }
    }

    public IEnumerable<Account> GetAll()
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            return context.Accounts.ToList();
        }
    }

    public bool IsAccountExist(string username)
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            Account? acc =  context.Accounts.FirstOrDefault(_ => _.Username == username);
            if(acc == null)
                return false;

            return true;
        }
    }

    public int Count()
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            return context.Accounts.Count();
        }
    }

    public void UpdatePasswordHash(int id, string passwordHash)
    {
        using (AccountContext context = _factory.CreateDbContext())
        {
            Account stored = context.Accounts.First(o => o.Id == id);

            stored.PasswordHash = passwordHash;

            context.SaveChanges();
        }
    }
}