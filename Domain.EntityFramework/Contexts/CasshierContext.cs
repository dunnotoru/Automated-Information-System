using Domain.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Domain.EntityFramework.Contexts
{
    public class CasshierContext : DbContext
    {
        public DbSet<RunEntity> Runs { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<PassportEntity> Passports { get; set; }
        public DbSet<RouteEntity> Routes { get; set; }
        public DbSet<TicketTypeEntity> TicketTypes { get; set; }

        public CasshierContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["SqliteConnection"].ConnectionString;

            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassportEntity>()
                .HasKey(p => new { p.Number, p.Series });

            modelBuilder.Entity<DriverEntity>()
                .HasKey(d => d.PayrollNumber);

            modelBuilder.Entity<RunEntity>()
                .HasKey(r => r.Number);

            modelBuilder.Entity<VehicleEntity>()
                .HasKey(v => v.LicenseNumber);
        }
    }
}
