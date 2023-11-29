﻿using Domain.EntityFramework.Configurations;
using Domain.Models;
using Domain.Models.Drivers;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace Domain.EntityFramework.Contexts
{
    public class CashierContext : DbContext
    {
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public CashierContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["DomainDatabase"].ConnectionString;

            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<Driver>(new DriverConfiguration());
            modelBuilder.ApplyConfiguration<Passport>(new PassportConfiguration());
            modelBuilder.ApplyConfiguration<Route>(new RouteConfiguration());
            modelBuilder.ApplyConfiguration<Run>(new RunConfiguration());
            modelBuilder.ApplyConfiguration<Station>(new StationConfiguration());
            modelBuilder.ApplyConfiguration<Vehicle>(new VehicleConfiguration());
        }
    }
}