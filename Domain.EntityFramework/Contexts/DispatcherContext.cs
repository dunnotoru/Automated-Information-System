using Domain.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Domain.EntityFramework.Contexts
{
    public class DispatcherContext : DbContext
    {
        public DbSet<StationEntity> Stations { get; set; }  
        public DbSet<RouteEntity> Routes { get; set; }  
        public DbSet<ScheduleLineEntity> ScheduleLines { get; set; }  
        public DbSet<DriverEntity> Drivers { get; set; }  

        public DispatcherContext()
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
            
        }
    }
}
