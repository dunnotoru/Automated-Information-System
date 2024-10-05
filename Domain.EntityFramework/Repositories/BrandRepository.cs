using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories;

public class BrandRepository : IBrandRepository
{
    public int Create(Brand entity)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            context.Brands.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            Brand storedEntity = context.Brands.First(o => o.Id == id);
            context.Brands.Remove(storedEntity);
            context.SaveChanges();
        }
    }

    public void Update(int id, Brand entity)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            Brand updatedEntity = context.Brands.First(o => o.Id == id);
            updatedEntity.Name = entity.Name;
            context.SaveChanges();
        }
    }
    public Brand GetById(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Brands.First(o => o.Id == id);
        }
    }

    public IEnumerable<Brand> GetAll()
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Brands.ToList();
        }
    }
}