using Companies.Application.Features.Companies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyPartnerConfig : IEntityTypeConfiguration<CompanyPartner>
{
    public void Configure(EntityTypeBuilder<CompanyPartner> builder)
    {
        builder.ToTable("CompanyPartners")
            .HasKey(ce => new { ce.CompanyId, ce.PartnerId });

        builder.Property(c => c.CompanyId).ValueGeneratedNever();
        builder.Property(c => c.PartnerId).ValueGeneratedNever();

        builder.HasOne(ce => ce.Qualification).WithMany().HasForeignKey(ce => ce.QualificationId);
        builder.HasOne(ce => ce.Partner).WithMany(p => p.Companies).HasForeignKey(ce => ce.PartnerId);
        builder.HasOne(ce => ce.Company).WithMany(p => p.Partners).HasForeignKey(ce => ce.CompanyId);
    }
}
