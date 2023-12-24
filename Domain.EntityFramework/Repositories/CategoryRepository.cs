using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public int Create(Category entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Categories.Add(entity);
                context.SaveChanges();
            }
            return entity.Id;
        }

        public void Update(int id, Category entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.Categories.Attach(entity);
                context.Categories.Update(entity);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Category entity = context.Categories.First(o => o.Id == id);
                context.Categories.Remove(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Categories.ToList();
            }
        }

        public Category GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Categories.First(o => o.Id == id);
            }
        }
    }
}
