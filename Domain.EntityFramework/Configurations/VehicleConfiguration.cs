using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations
{
    internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.LicensePlateNumber).IsUnique();

            builder
                .HasOne(l => l.VehicleModel)
                .WithMany(r => r.Vehicles)
                .HasForeignKey(o => o.VehicleModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(l => l.RepairType)
                .WithMany(r => r.Vehicles)
                .HasForeignKey(o => o.RepairTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(l => l.Freighter)
                .WithMany(r => r.Vehicles)
                .HasForeignKey(o => o.FreighterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
