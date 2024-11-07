using System.Reflection;
using Companies.Application.Abstractions.Database;
using Companies.Application.Features.Companies;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Contexts;

public class CompaniesContext(DbContextOptions<CompaniesContext> options) 
    : DbContext(options), ICompaniesContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyPartner> CompanyPartners => Set<CompanyPartner>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
