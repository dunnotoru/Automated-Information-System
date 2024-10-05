using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class TicketRepository : ITicketRepository
{
    public int Create(Ticket entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = new ApplicationContext())
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
        using (ApplicationContext context = new ApplicationContext())
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
        using (ApplicationContext context = new ApplicationContext())
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
        using (ApplicationContext context = new ApplicationContext())
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
        using (ApplicationContext context = new ApplicationContext())
        {
            Ticket stored = context.Tickets.First(o => o.Id == id);
            stored = entity;
            stored.Id = id;
            context.Tickets.Update(entity);
            context.SaveChanges();
        }
    }
}