using Companies.Application.Features.Companies;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Abstractions.Database;

public interface ICompaniesContext
{
    public DbSet<Company> Companies { get; }
    public DbSet<CompanyPartner> CompanyPartners { get; }
}

