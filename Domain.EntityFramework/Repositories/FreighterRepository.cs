
using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class FreighterRepository : IFreighterRepository
    {
        public int Create(Freighter entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Freighters.Add(entity);
                context.SaveChanges();
            }
            return entity.Id;
        }

        public IEnumerable<Freighter> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Freighters.ToList();
            }
        }

        public Freighter GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Freighters.First(o => o.Id == id);
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Freighter stored = context.Freighters.First(o => o.Id == id);
                context.Freighters.Remove(stored);
            }
        }

        public void Update(int id, Freighter entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Freighter updatedEntity = new Freighter();
                updatedEntity.Name = entity.Name;
                context.Freighters.Add(updatedEntity);
                context.SaveChanges();
            }
        }
    }
}
