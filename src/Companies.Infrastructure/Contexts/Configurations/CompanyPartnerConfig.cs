using Companies.Domain.Features.Companies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyPartnerConfig : IEntityTypeConfiguration<CompanyPartner>
{
    public void Configure(EntityTypeBuilder<CompanyPartner> builder)
    {
        builder.ToTable("CompanyPartners")
            .HasKey(ce => new { ce.CompanyId, ce.PartnerId });

        builder.HasOne(ce => ce.Qualification).WithMany().HasForeignKey(ce => ce.QualificationId);
        builder.HasOne(ce => ce.Partner).WithMany().HasForeignKey(ce => ce.PartnerId);
        builder.HasOne(ce => ce.Company).WithMany().HasForeignKey(ce => ce.CompanyId);
    }
}
