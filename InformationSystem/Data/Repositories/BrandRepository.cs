using System.Collections.Generic;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Data.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public BrandRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(Brand entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            context.Brands.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            Brand storedEntity = context.Brands.First(o => o.Id == id);
            context.Brands.Remove(storedEntity);
            context.SaveChanges();
        }
    }

    public void Update(int id, Brand entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            Brand updatedEntity = context.Brands.First(o => o.Id == id);
            updatedEntity.Name = entity.Name;
            context.SaveChanges();
        }
    }
    public Brand GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Brands.First(o => o.Id == id);
        }
    }

    public IEnumerable<Brand> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Brands.ToList();
        }
    }
}