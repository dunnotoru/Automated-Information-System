using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class PassportRepository : IPassportRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public PassportRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(IdentityDocument entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            entity.Name = entity.Name.ToLower();
            entity.Surname = entity.Surname.ToLower();
            entity.Patronymic = entity.Patronymic.ToLower();
            entity.BirthDate = entity.BirthDate.Date;
                
            context.Passports.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }
    public void Update(int id, IdentityDocument entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            IdentityDocument stored = context.Passports.First(o => o.Id == id);

            stored.Id = id;
            stored.Name = entity.Name.ToLower();
            stored.Surname = entity.Surname.ToLower();
            stored.Patronymic = entity.Patronymic.ToLower();
            stored.BirthDate = entity.BirthDate.Date;

            context.Passports.Update(stored);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            IdentityDocument stored = context.Passports.First(o => o.Id == id);

            context.Passports.Remove(stored);
            context.SaveChanges();
        }
    }

    public IdentityDocument GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Passports.First(o => o.Id == id);
        }
    }

    public IdentityDocument Get(string number, string series)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Passports.First(x => x.Number == number && x.Series == series);
        }
    }

    public IEnumerable<IdentityDocument> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Passports.ToList();
        }
    }

    public bool IsExist(IdentityDocument document)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            IdentityDocument? stored = 
                context.Passports
                    .FirstOrDefault(o => o.Series == document.Series 
                                         && o.Number == document.Number);

            if (stored == null) return false;

            if (stored.Name.ToLower() == document.Name.ToLower() &&
                stored.Surname.ToLower() == document.Surname.ToLower() &&
                stored.Patronymic.ToLower() == document.Patronymic.ToLower() &&
                stored.BirthDate.Date == document.BirthDate)
            {
                return true;
            }

            throw new InvalidDataException("Неверные паспортные данные");
        }
    }
}