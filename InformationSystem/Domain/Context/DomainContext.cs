using InformationSystem.Domain.Configurations;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Domain.Context;

public sealed class DomainContext : DbContext
{
    public DbSet<Account> Accounts { get; init; }
    
    public DbSet<Run> Runs { get; init; }
    public DbSet<IdentityDocument> Passports { get; init; }
    public DbSet<Route> Routes { get; init; }
    public DbSet<Station> Stations { get; init; }
    public DbSet<Schedule> Schedules { get; init; }
    public DbSet<Vehicle> Vehicles { get; init; }
    public DbSet<Driver> Drivers { get; init; }
    public DbSet<Ticket> Tickets { get; init; }
    public DbSet<DriverLicense> Licenses { get; init; }


    public DbSet<StationRoute> StationRoute { get; init; }
    public DbSet<LicenseCategory> LicenseCategory { get; init; }

    public DbSet<Brand> Brands { get; init; }
    public DbSet<VehicleModel> VehicleModels { get; init; }
    public DbSet<RepairType> RepairTypes { get; init; }
    public DbSet<Freighter> Freighters { get; init; }
    public DbSet<TicketType> TicketTypes { get; init; }
    public DbSet<Category> Categories { get; init; }

    public DomainContext(DbContextOptions<DomainContext> options) : base(options)
    {
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new DriverConfiguration());
        modelBuilder.ApplyConfiguration(new PassportConfiguration());
        modelBuilder.ApplyConfiguration(new RouteConfiguration());
        modelBuilder.ApplyConfiguration(new RunConfiguration());
        modelBuilder.ApplyConfiguration(new StationConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        modelBuilder.ApplyConfiguration(new DriverLicenseConfiguration());
        modelBuilder.ApplyConfiguration(new TicketTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
    }
}