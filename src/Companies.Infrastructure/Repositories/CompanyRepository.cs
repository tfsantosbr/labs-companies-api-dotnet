using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Repositories;
using Companies.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class CompanyRepository(CompaniesContext context) : ICompanyRepository
{
    public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
    {
        await context.Companies.AddAsync(company, cancellationToken);
    }

    public async Task<bool> AnyByCnpjAsync(
        string cnpj, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await context.Companies
            .AsNoTracking()
            .AnyAsync(company =>
                company.Cnpj.Number == cnpj &&
                (ignoredId == null || company.Id == ignoredId), 
                cancellationToken);
    }

    public async Task<bool> AnyByNameAsync(
        string name, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await context.Companies
            .AsNoTracking()
            .AnyAsync(company =>
                company.Name == name &&
                (ignoredId == null || company.Id == ignoredId), 
                cancellationToken);
    }

    public async Task<bool> AnyByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await context.Companies
            .AsNoTracking()
            .AnyAsync(company => company.Id == companyId, cancellationToken);
    }

    public async Task<Company?> GetByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await context.Companies
            .Include(p => p.Phones)
            .Include(p => p.Partners)
            .FirstOrDefaultAsync(c => c.Id == companyId, 
                cancellationToken);
    }

    public void Remove(Company company)
    {
        context.Companies.Remove(company);
    }
}
