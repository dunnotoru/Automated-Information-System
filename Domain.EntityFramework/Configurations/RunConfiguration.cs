using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations
{
    internal class RunConfiguration : IEntityTypeConfiguration<Run>
    {
        public void Configure(EntityTypeBuilder<Run> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(o => o.Number).IsUnique();

            builder
                .HasOne(l => l.Driver)
                .WithOne(r => r.Run)
                .HasForeignKey<Run>(o => o.DriverId)
                .OnDelete(DeleteBehavior.SetNull)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(l => l.Vehicle)
                .WithOne(r => r.Run)
                .HasForeignKey<Run>(o => o.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
