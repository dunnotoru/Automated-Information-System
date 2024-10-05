using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories;

public class RepairTypeRepository : IRepairTypeRepository
{
    public int Create(RepairType entity)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            context.RepairTypes.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public IEnumerable<RepairType> GetAll()
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.RepairTypes.ToList();
        }
    }

    public RepairType GetById(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.RepairTypes.First(o => o.Id == id);
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            RepairType stored = context.RepairTypes.First(o => o.Id == id);
            context.RepairTypes.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, RepairType entity)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            RepairType updatedEntity = context.RepairTypes.First(o => o.Id == id);
            updatedEntity.Name = entity.Name;
            context.SaveChanges();
        }
    }
}