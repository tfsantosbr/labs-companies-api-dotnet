using Companies.Application.Features.CompanyMainActivities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyMainActivityConfig : IEntityTypeConfiguration<CompanyMainActivity>
{
    public void Configure(EntityTypeBuilder<CompanyMainActivity> builder)
    {
        builder.ToTable("CompanyMainActivities").HasKey(cma => cma.Code);

        builder.Property(cma => cma.Code).ValueGeneratedNever();
        builder.Property(cma => cma.Description).HasMaxLength(300);
    }
}
