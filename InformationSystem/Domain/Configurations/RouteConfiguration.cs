using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InformationSystem.Data.Configurations;

internal sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.HasKey(x => x.Id);
            
        builder
            .HasMany(r => r.Stations)
            .WithMany(s => s.Routes)
            .UsingEntity<StationRoute>();

        builder
            .HasMany(l => l.Runs)
            .WithOne(r => r.Route)
            .HasForeignKey(o => o.RouteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}