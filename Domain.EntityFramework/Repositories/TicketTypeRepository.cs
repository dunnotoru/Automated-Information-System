using Domain.EntityFramework.Context;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class TicketTypeRepository : ITicketTypeRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public TicketTypeRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(TicketType entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            context.TicketTypes.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, TicketType entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.TicketTypes.Attach(entity);
            context.TicketTypes.Update(entity);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            TicketType stored = context.TicketTypes.First(o => o.Id == id);
            context.Remove(stored);
            context.SaveChanges();
        }
    }
    public TicketType GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.TicketTypes.First(o => o.Id == id);
        }
    }

    public IEnumerable<TicketType> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.TicketTypes.ToList();
        }
    }
}