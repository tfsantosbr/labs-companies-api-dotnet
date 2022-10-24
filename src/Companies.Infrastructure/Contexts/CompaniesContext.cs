using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Companies.Domain.Features.CompanyMainActivities;
using Companies.Domain.Features.CompanyPartnerQualifications;
using Companies.Domain.Features.Partners;
using Companies.Domain.Features.Companies;

namespace Companies.Infrastructure.Contexts;

public class CompaniesContext : DbContext
{
    public CompaniesContext(DbContextOptions<CompaniesContext> options) : base(options)
    {
    }

    public DbSet<CompanyMainActivity> CompanyMainActivities => Set<CompanyMainActivity>();
    public DbSet<CompanyPartnerQualification> CompanyPartnerQualifications => Set<CompanyPartnerQualification>();
    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<Company> Companies => Set<Company>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
