using Companies.Application.Features.Companies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyPhoneConfig : IEntityTypeConfiguration<CompanyPhone>
{
    public void Configure(EntityTypeBuilder<CompanyPhone> builder)
    {
        builder.ToTable("CompanyPhones").HasKey(cp => cp.Id);

        builder.Property(cp => cp.Id).ValueGeneratedNever();
        builder.Property(cp => cp.CompanyId).ValueGeneratedNever();

        builder.OwnsOne(cp => cp.Phone, phoneBuilder =>
        {
            phoneBuilder.Property(p => p.CountryCode).HasColumnName("CountryCode").HasMaxLength(3);
            phoneBuilder.Property(p => p.Number).HasColumnName("Number").HasMaxLength(10);

            phoneBuilder.HasIndex(p => new { p.CountryCode, p.Number }).IsUnique();
        });
    }
}
