using Domain.EntityFramework.Context;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class RepairTypeRepository : IRepairTypeRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;
    
    public int Create(RepairType entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            context.RepairTypes.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public IEnumerable<RepairType> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.RepairTypes.ToList();
        }
    }

    public RepairType GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.RepairTypes.First(o => o.Id == id);
        }
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            RepairType stored = context.RepairTypes.First(o => o.Id == id);
            context.RepairTypes.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, RepairType entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            RepairType updatedEntity = context.RepairTypes.First(o => o.Id == id);
            updatedEntity.Name = entity.Name;
            context.SaveChanges();
        }
    }
}