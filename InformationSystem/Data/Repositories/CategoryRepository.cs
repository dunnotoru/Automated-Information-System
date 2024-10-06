using System.Collections.Generic;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public CategoryRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(Category entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, Category entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.Categories.Attach(entity);
            context.Categories.Update(entity);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            Category entity = context.Categories.First(o => o.Id == id);
            context.Categories.Remove(entity);
            context.SaveChanges();
        }
    }

    public IEnumerable<Category> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Categories.ToList();
        }
    }

    public Category GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Categories.First(o => o.Id == id);
        }
    }
}