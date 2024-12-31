using Companies.Application.Features.CompanyPartnerQualifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyPartnerQualificationConfig : IEntityTypeConfiguration<CompanyPartnerQualification>
{
    public void Configure(EntityTypeBuilder<CompanyPartnerQualification> builder)
    {
        builder.ToTable("CompanyPartnerQualifications").HasKey(cpq => cpq.Code);

        builder.Property(cpq => cpq.Code).ValueGeneratedNever();
        builder.Property(cpq => cpq.Description).HasMaxLength(300);
    }
}
