using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Companies.Domain.Features.CompanyPartnerQualifications;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyPartnerQualificationConfig : IEntityTypeConfiguration<CompanyPartnerQualification>
{
    public void Configure(EntityTypeBuilder<CompanyPartnerQualification> builder)
    {
        builder.ToTable("CompanyPartnerQualifications").HasKey(cpq => cpq.Code);
        
        builder.Property(cpq => cpq.Code).ValueGeneratedNever();
        builder.Property(cpq => cpq.Description).HasColumnType("varchar").HasMaxLength(300);
    }
}
