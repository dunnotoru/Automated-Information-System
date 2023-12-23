using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations
{
    internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasIndex(o => o.PayrollNumber).IsUnique();
            builder.HasIndex(o => o.EmploymentBookDetails).IsUnique();

            builder
                .HasOne(l => l.DriverLicense)
                .WithOne(r => r.Driver)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<DriverLicense>(o => o.DriverId);
        }
    }
}
