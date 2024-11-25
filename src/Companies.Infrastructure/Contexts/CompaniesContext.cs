using System.Reflection;
using Companies.Application.Abstractions.Database;
using Companies.Application.Features.Companies;
using Companies.Application.Features.CompanyMainActivities;
using Companies.Application.Features.CompanyPartnerQualifications;
using Companies.Application.Features.Partners;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Contexts;

public class CompaniesContext(DbContextOptions<CompaniesContext> options) 
    : DbContext(options), ICompaniesContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyPartner> CompanyPartners => Set<CompanyPartner>();
    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<CompanyMainActivity> CompanyMainActivities => Set<CompanyMainActivity>();
    public DbSet<CompanyPartnerQualification> CompanyPartnerQualifications => Set<CompanyPartnerQualification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
