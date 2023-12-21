using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityFramework.Configurations
{
    internal class DriverLicenseConfiguration : IEntityTypeConfiguration<DriverLicense>
    {
        public void Configure(EntityTypeBuilder<DriverLicense> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasMany(o => o.Categories)
                .WithMany(o => o.Licenses)
                .UsingEntity<LicenseCategory>();
        }
    }
}
