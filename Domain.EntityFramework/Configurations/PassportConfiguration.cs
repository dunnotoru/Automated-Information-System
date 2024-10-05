using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations;

internal sealed class PassportConfiguration : IEntityTypeConfiguration<IdentityDocument>
{
    public void Configure(EntityTypeBuilder<IdentityDocument> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasIndex(o => new { o.Series, o.Number }).IsUnique();
        builder.HasIndex(o => new { o.Series, o.Number, o.Name, o.Surname, o.Patronymic, o.BirthDate }).IsUnique();
    }
}