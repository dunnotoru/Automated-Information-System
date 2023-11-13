using Domain.Models;
using Domain.Models.Drivers;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Reflection;

namespace Domain.EntityFramework.Contexts
{
    public class CasshierContext : DbContext
    {
        public DbSet<Run> Runs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        public CasshierContext()
        {
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

            modelBuilder.Entity<Driver>()
                .HasKey(d => d.PayrollNumber);

            modelBuilder.Entity<Run>()
                .HasKey(r => r.Number);

            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.LicensePlateNumber);
        }
    }
}
