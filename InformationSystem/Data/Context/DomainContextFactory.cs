using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InformationSystem.Data.Context;

public sealed class DomainContextFactory : IDesignTimeDbContextFactory<DomainContext>
{
    public DomainContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<DomainContext> optionsBuilder = new DbContextOptionsBuilder<DomainContext>();
        
        IConfigurationBuilder cfgBuilder = new ConfigurationBuilder();
        cfgBuilder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("migrationsettings.json");
        
        IConfigurationRoot cfg = cfgBuilder.Build();
        
        
        string connectionString = cfg.GetConnectionString("DomainDatabase")
                                  ?? throw new NullReferenceException("Connection string is null");
        
        optionsBuilder.UseSqlite("Data Source=" + connectionString);

        return new DomainContext(optionsBuilder.Options);
    }
}