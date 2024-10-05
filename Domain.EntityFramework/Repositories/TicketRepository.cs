using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public TicketRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(Ticket entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            context.TicketTypes.Attach(entity.TicketType);
            context.Runs.Attach(entity.Run);
            context.Passports.Attach(entity.IdentityDocument);
            context.Tickets.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;   
    }

    public IEnumerable<Ticket> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Tickets
                .Include(o => o.Run)
                .Include(o => o.IdentityDocument)
                .Include(o => o.TicketType)
                .ToList();
        }
    }

    public Ticket GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Tickets
                .Include(o => o.Run)
                .Include(o => o.IdentityDocument)
                .Include(o => o.TicketType)
                .First(o => o.Id == id);
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Ticket stored = context.Tickets
                .First(o => o.Id == id);
            context.Tickets.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, Ticket entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Ticket stored = context.Tickets.First(o => o.Id == id);
            stored = entity;
            stored.Id = id;
            context.Tickets.Update(entity);
            context.SaveChanges();
        }
    }
}