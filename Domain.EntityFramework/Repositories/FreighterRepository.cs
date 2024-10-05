
using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class FreighterRepository : IFreighterRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public FreighterRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(Freighter entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            context.Freighters.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public IEnumerable<Freighter> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Freighters.ToList();
        }
    }

    public Freighter GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Freighters.First(o => o.Id == id);
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Freighter stored = context.Freighters.First(o => o.Id == id);
            context.Freighters.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, Freighter entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Freighter updatedEntity = new Freighter();
            updatedEntity.Name = entity.Name;
            context.Freighters.Add(updatedEntity);
            context.SaveChanges();
        }
    }
}