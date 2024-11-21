using Companies.Application.Features.Companies;
using Companies.Application.Features.Partners;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Abstractions.Database;

public interface ICompaniesContext
{
    public DbSet<Company> Companies { get; }
    public DbSet<CompanyPartner> CompanyPartners { get; }
    public DbSet<Partner> Partners { get; }
}

