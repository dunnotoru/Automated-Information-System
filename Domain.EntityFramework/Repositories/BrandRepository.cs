using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public BrandRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(Brand entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            context.Brands.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Brand storedEntity = context.Brands.First(o => o.Id == id);
            context.Brands.Remove(storedEntity);
            context.SaveChanges();
        }
    }

    public void Update(int id, Brand entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Brand updatedEntity = context.Brands.First(o => o.Id == id);
            updatedEntity.Name = entity.Name;
            context.SaveChanges();
        }
    }
    public Brand GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Brands.First(o => o.Id == id);
        }
    }

    public IEnumerable<Brand> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Brands.ToList();
        }
    }
}