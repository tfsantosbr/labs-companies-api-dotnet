using Companies.Application.Features.Companies;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Companies.Infrastructure.Contexts.Configurations;

public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies").HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(500);

        builder.OwnsOne(c => c.Cnpj, cnpj =>
        {
            cnpj.Property(c => c.Number)
                .HasColumnName("Cnpj").IsRequired().HasMaxLength(14);

            cnpj.HasIndex(c => c.Number).IsUnique();
        });

        builder.OwnsOne(p => p.Address, address =>
        {
            address.Property(p => p.PostalCode).HasColumnName("PostalCode").IsRequired().HasMaxLength(15);
            address.Property(p => p.Street).HasColumnName("Street").IsRequired().HasMaxLength(200);
            address.Property(p => p.Number).HasColumnName("AddressNumber").IsRequired().HasMaxLength(10);
            address.Property(p => p.Complement).HasColumnName("Complement").HasMaxLength(120);
            address.Property(p => p.Neighborhood).HasColumnName("Neighborhood").IsRequired().HasMaxLength(100);
            address.Property(p => p.City).HasColumnName("City").IsRequired().HasMaxLength(80);
            address.Property(p => p.State).HasColumnName("State").IsRequired().HasMaxLength(3);
            address.Property(p => p.Country).HasColumnName("Country").IsRequired().HasMaxLength(60);
        });

        builder.HasOne(c => c.MainActivity).WithMany().HasForeignKey(c => c.MainActivityId);
        builder.HasMany(c => c.Phones).WithOne().HasForeignKey(c => c.CompanyId);

        builder.HasIndex(c => c.Name).IsUnique();
    }
}
