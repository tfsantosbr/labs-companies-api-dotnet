using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Companies.Domain.Features.CompanyEmployeePositions;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyEmployeePositionConfig : IEntityTypeConfiguration<CompanyEmployeePosition>
{
    public void Configure(EntityTypeBuilder<CompanyEmployeePosition> builder)
    {
        builder.ToTable("CompanyEmployeePositions").HasKey(cep => cep.Id);
        builder.Property(cep => cep.Description).HasColumnType("varchar").HasMaxLength(300);
    }
}
