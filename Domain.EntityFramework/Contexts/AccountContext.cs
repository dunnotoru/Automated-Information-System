using Domain.EntityFramework.Configurations;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Contexts;

public sealed class AccountContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Account>(new AccountConfiguration());
    }
}