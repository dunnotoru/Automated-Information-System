using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations
{
    internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(l => l.License)
                .WithOne(r => r.Driver)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<DriverLicense>(e => e.DriverId);
        }
    }
}
