﻿using Domain.EntityFramework.Configurations;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Domain.EntityFramework.Contexts
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverLicense> Licenses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<StationRoute> StationRoute { get; set; }
        public DbSet<LicenseCategory> LicenseCategory { get; set; }

        public ApplicationContext()
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
            modelBuilder.ApplyConfiguration<DriverLicense>(new DriverLicenseConfiguration());
            modelBuilder.ApplyConfiguration<TicketType>(new TicketTypeConfiguration());
        }
    }
}
