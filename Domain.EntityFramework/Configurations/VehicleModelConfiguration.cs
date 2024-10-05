using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations;

internal class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
{
    public void Configure(EntityTypeBuilder<VehicleModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(l => l.Brand)
            .WithMany(r => r.VehicleModels)
            .HasForeignKey(o => o.BrandId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}