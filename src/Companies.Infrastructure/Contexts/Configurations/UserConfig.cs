using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Companies.Domain.Features.Users;

namespace Companies.Infrastructure.Contexts.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.OwnsOne(p => p.CompleteName, completeName =>
        {
            completeName.Property(p => p.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(300);
            completeName.Property(p => p.Lastname).HasColumnName("Lastname").IsRequired().HasMaxLength(300);
        });

        builder.OwnsOne(p => p.Email, email =>
        {
            email.Property(p => p.Address).HasColumnName("Email").IsRequired().HasMaxLength(300);
            email.HasIndex(p => p.Address).IsUnique();
        });
    }
}
