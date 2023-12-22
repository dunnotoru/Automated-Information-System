using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        public void Create(TicketType entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.TicketTypes.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(int id, TicketType entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.TicketTypes.Attach(entity);
                context.TicketTypes.Update(entity);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                TicketType stored = context.TicketTypes.First(o => o.Id == id);
                context.Remove(stored);
                context.SaveChanges();
            }
        }
        public TicketType GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.TicketTypes.First(o => o.Id == id);
            }
        }

        public IEnumerable<TicketType> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.TicketTypes.ToList();
            }
        }
    }
}
