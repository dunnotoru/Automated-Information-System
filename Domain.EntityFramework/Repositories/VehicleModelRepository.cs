using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace Domain.EntityFramework.Repositories
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        public int Create(VehicleModel entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                VehicleModel createdEntity = new VehicleModel();
                Brand brand = context.Brands.First(o => o.Id == entity.Brand.Id);
                createdEntity.Name = entity.Name;
                createdEntity.Brand = brand;
                createdEntity.Capacity = entity.Capacity;

                context.VehicleModels.Add(createdEntity);
                context.SaveChanges();
            }
            return entity.Id;
        }
        public void Update(int id, VehicleModel entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                VehicleModel updatedEntity = context.VehicleModels.First(o => o.Id == id);
                Brand brand = context.Brands.First(o => o.Id == entity.Brand.Id);
                updatedEntity.Name = entity.Name;
                updatedEntity.Brand = brand;
                updatedEntity.Capacity = entity.Capacity;

                context.SaveChanges();
            }
        }

        public IEnumerable<VehicleModel> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.VehicleModels.Include(o => o.Brand).ToList();
            }
        }

        public VehicleModel GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.VehicleModels.Include(o => o.Brand).First(o => o.Id == id);
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                VehicleModel stored = context.VehicleModels.First(o => o.Id == id);
                context.VehicleModels.Remove(stored);
                context.SaveChanges();
            }
        }

    }
}
