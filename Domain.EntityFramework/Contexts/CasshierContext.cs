using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace Domain.EntityFramework.Contexts
{
    public class CasshierContext : DbContext
    {
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public CasshierContext()
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
