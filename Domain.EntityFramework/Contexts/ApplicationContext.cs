using Domain.EntityFramework.Configurations;
using Domain.Models;
using Domain.Models.Drivers;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace Domain.EntityFramework.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
