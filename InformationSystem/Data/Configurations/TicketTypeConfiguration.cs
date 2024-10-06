using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InformationSystem.Data.Configurations;

internal class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasIndex(o => o.Name).IsUnique();
    }
}