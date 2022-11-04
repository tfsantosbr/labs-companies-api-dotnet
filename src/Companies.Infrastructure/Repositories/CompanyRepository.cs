using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DbSet<Company> _companies;

    public CompanyRepository(CompaniesContext context)
    {
        _companies = context.Companies;
    }

    public async Task Add(Company company)
    {
        await _companies.AddAsync(company);
    }

    public async Task<bool> AnyByCnpj(string cnpj, Guid? ignoredId = null)
    {
        return await _companies
            .AsNoTracking()
            .AnyAsync(company =>
                company.Cnpj.Number == cnpj &&
                (ignoredId == null || company.Id == ignoredId)
            );
    }

    public async Task<bool> AnyByName(string name, Guid? ignoredId = null)
    {
        return await _companies
            .AsNoTracking()
            .AnyAsync(company =>
                company.Name == name &&
                (ignoredId == null || company.Id == ignoredId)
            );
    }

    public async Task<Company?> GetById(Guid companyId)
    {
        return await _companies.FirstOrDefaultAsync(c => c.Id == companyId);
    }
}
