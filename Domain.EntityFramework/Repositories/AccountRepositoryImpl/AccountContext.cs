using Domain.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories.AccountRepositoryImpl
{
    public class AccountContext : DbContext
    {
        private string _connectionString;
        public DbSet<AccountEntity> Accounts { get; set; } = null!;
        public AccountContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
