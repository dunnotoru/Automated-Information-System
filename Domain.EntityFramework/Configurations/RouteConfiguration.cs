using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net.NetworkInformation;

namespace Domain.EntityFramework.Configurations
{
    internal sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasMany(r => r.Stations)
                .WithMany(s => s.Routes)
                .UsingEntity<StationRoute>(
                l => l.HasOne<Station>().WithMany().HasForeignKey(e => e.StationId),
                r => r.HasOne<Route>().WithMany().HasForeignKey(e => e.RouteId));
        }
    }
}
