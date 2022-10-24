using Companies.Domain.Features.Companies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyPhoneConfig : IEntityTypeConfiguration<CompanyPhone>
{
    public void Configure(EntityTypeBuilder<CompanyPhone> builder)
    {
        builder.ToTable("CompanyPhones").HasKey(cp => new { cp.CompanyId, cp.Phone.Number });

        builder.OwnsOne(cp => cp.Phone, phoneBuilder =>
        {
            phoneBuilder.Property(p => p.Number).HasColumnType("varchar").HasMaxLength(10);
            phoneBuilder.HasIndex(p => p.Number).IsUnique();
        });
    }
}
