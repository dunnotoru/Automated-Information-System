using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace Domain.EntityFramework.Contexts
{
    internal class DispatcherContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }  
        //public DbSet<Route> Routes { get; set; }  
        //public DbSet<Driver> Drivers { get; set; }  

        public DispatcherContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["SqliteConnection"].ConnectionString;

            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetCallingAssembly());
        }
    }
}
