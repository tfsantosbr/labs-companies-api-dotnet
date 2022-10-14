using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Companies.Domain.Features.CompanyEmployeePositions;
using Companies.Domain.Features.CompanyMainActivities;
using Companies.Domain.Features.CompanyPartnerQualifications;

namespace Companies.Infrastructure.Contexts;

public class CompaniesContext : DbContext
{
    public CompaniesContext(DbContextOptions<CompaniesContext> options) : base(options)
    {
    }

    public DbSet<CompanyEmployeePosition> CompanyEmployeePositions => Set<CompanyEmployeePosition>();
    public DbSet<CompanyMainActivity> CompanyMainActivities => Set<CompanyMainActivity>();
    public DbSet<CompanyPartnerQualification> CompanyPartnerQualifications => Set<CompanyPartnerQualification>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
