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
                .HasMany(l => l.Drivers)
                .WithOne(r => r.Run)
                .HasForeignKey(o => o.RunId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(l => l.Vehicle)
                .WithOne(r => r.Run)
                .HasForeignKey<Run>(o => o.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
