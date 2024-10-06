using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InformationSystem.Data.Configurations;

internal sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(l => l.Route)
            .WithMany(r => r.Schedules)
            .HasForeignKey(o => o.RouteId);

        builder
            .HasOne(l => l.Run)
            .WithOne(r => r.Schedule)
            .HasForeignKey<Schedule>(o => o.RunId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}