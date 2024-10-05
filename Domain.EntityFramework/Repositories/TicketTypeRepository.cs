using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class TicketTypeRepository : ITicketTypeRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public TicketTypeRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(TicketType entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            context.TicketTypes.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, TicketType entity)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.TicketTypes.Attach(entity);
            context.TicketTypes.Update(entity);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            TicketType stored = context.TicketTypes.First(o => o.Id == id);
            context.Remove(stored);
            context.SaveChanges();
        }
    }
    public TicketType GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.TicketTypes.First(o => o.Id == id);
        }
    }

    public IEnumerable<TicketType> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.TicketTypes.ToList();
        }
    }
}