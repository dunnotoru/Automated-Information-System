using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public void Add(Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Ticket stored = context.Tickets.First(o => o.Id == id);
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
}
