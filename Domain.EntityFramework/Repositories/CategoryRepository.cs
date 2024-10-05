using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public CategoryRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(Category entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            context.Categories.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, Category entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.Categories.Attach(entity);
            context.Categories.Update(entity);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Category entity = context.Categories.First(o => o.Id == id);
            context.Categories.Remove(entity);
            context.SaveChanges();
        }
    }

    public IEnumerable<Category> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Categories.ToList();
        }
    }

    public Category GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Categories.First(o => o.Id == id);
        }
    }
}