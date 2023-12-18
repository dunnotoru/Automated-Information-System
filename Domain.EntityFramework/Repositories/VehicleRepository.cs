﻿using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public void Add(Vehicle entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Vehicles.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Vehicle stored = context.Vehicles.Single(o => o.Id == id);
                context.Vehicles.Remove(stored);
                context.SaveChanges();
            }
        }

        public void Update(int id, Vehicle entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.Vehicles.Attach(entity);
                context.Vehicles.Update(entity);
                context.SaveChanges();
            }
        }

        public Vehicle GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Vehicles.Single(o => o.Id == id);
            }
        }

        public Vehicle GetByLicenseNumber(string licensePlateNumber)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Vehicles.Single(o => o.LicensePlateNumber == licensePlateNumber);
            }
        }

        public IEnumerable<Vehicle> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Vehicles.ToList();
            }
        }
    }
}