using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Models;
using Companies.Application.Features.Companies.Repositories;
using Companies.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    // Fields

    private readonly DbSet<Company> _companies;
    private readonly DbSet<CompanyPartner> _companyPartners;

    // Constructors

    public CompanyRepository(CompaniesContext context)
    {
        _companies = context.Set<Company>();
        _companyPartners = context.Set<CompanyPartner>();
    }

    // Implementations

    public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
    {
        await _companies.AddAsync(company, cancellationToken);
    }

    public async Task<bool> AnyByCnpjAsync(string cnpj, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await _companies
            .AsNoTracking()
            .AnyAsync(company =>
                company.Cnpj.Number == cnpj &&
                (ignoredId == null || company.Id == ignoredId), 
                cancellationToken);
    }

    public async Task<bool> AnyByNameAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default)
    {
        return await _companies
            .AsNoTracking()
            .AnyAsync(company =>
                company.Name == name &&
                (ignoredId == null || company.Id == ignoredId), 
                cancellationToken);
    }

    public async Task<bool> AnyByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _companies
            .AsNoTracking()
            .AnyAsync(company => company.Id == companyId, cancellationToken);
    }

    public async Task<Company?> GetByIdAsync(Guid companyId, CancellationToken cancellationToken = default)
    {
        return await _companies
            .Include(p => p.Phones)
            .Include(p => p.Partners)
            .FirstOrDefaultAsync(c => c.Id == companyId, 
                cancellationToken);
    }

    public void Remove(Company company)
    {
        _companies.Remove(company);
    }

    
    public async Task<IEnumerable<CompanyPartnerModel>> GetPartners(Guid companyId)
    {
        return await _companyPartners
            .AsNoTracking()
            .Where(cp => cp.CompanyId == companyId)
            .Select(cp => new CompanyPartnerModel(cp.PartnerId, cp.QualificationId, cp.JoinedAt.ToDateTime(TimeOnly.MinValue)))
            .ToListAsync();
    }

    
}
