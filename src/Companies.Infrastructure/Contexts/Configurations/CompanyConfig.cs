using Companies.Domain.Features.Companies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies").HasKey(c => c.Id);

        builder.Property(c => c.Name).HasColumnType("varchar").HasMaxLength(500);

        builder.OwnsOne(c => c.Cnpj, cnpj =>
        {
            cnpj.Property(c => c.Number)
                .HasColumnName("Cnpj").HasColumnType("varchar").IsRequired().HasMaxLength(300);

            cnpj.HasIndex(c => c.Number).IsUnique();
        });

        builder.OwnsOne(p => p.Address, address =>
        {
            address.Property(p => p.PostalCode).HasColumnName("PostalCode").HasMaxLength(15);
            address.Property(p => p.Street).HasColumnName("Street").HasMaxLength(200);
            address.Property(p => p.Number).HasColumnName("AddressNumber").HasMaxLength(10);
            address.Property(p => p.Complement).HasColumnName("Complement").HasMaxLength(120);
            address.Property(p => p.Neighborhood).HasColumnName("Neighborhood").HasMaxLength(100);
            address.Property(p => p.City).HasColumnName("City").HasMaxLength(80);
            address.Property(p => p.State).HasColumnName("State").HasMaxLength(3);
            address.Property(p => p.Country).HasColumnName("Country").HasMaxLength(60);
        });

        builder.HasOne(c => c.MainActivity).WithMany().HasForeignKey(c => c.MainActivityId);
        builder.HasMany(c => c.Partners).WithOne(cp => cp.Company).HasForeignKey(cp => cp.CompanyId);
        builder.HasMany(c => c.Phones).WithOne().HasForeignKey(c => c.CompanyId);

        builder.HasIndex(c => c.Name).IsUnique();
    }
}
