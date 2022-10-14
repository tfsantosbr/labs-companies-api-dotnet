using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Companies.Domain.Features.CompanyMainActivities;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyMainActivityConfig : IEntityTypeConfiguration<CompanyMainActivity>
{
    public void Configure(EntityTypeBuilder<CompanyMainActivity> builder)
    {
        builder.ToTable("CompanyMainActivities").HasKey(cma => cma.Code);
        
        builder.Property(cma => cma.Code).ValueGeneratedNever();
        builder.Property(cma => cma.Description).HasColumnType("varchar").HasMaxLength(300);
    }
}
