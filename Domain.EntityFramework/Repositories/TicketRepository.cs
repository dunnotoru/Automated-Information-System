using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public int Create(Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.TicketType = context.TicketTypes.First();
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
                return context.Tickets.ToList();
            }
        }

        public Ticket GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Tickets.First(o => o.Id == id);
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
