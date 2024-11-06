using Companies.Application.Base.Pagination;
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
        return await _companies
            .Include(p => p.Phones)
            .Include(p => p.Partners)
            .FirstOrDefaultAsync(c => c.Id == companyId);
    }

    public void Remove(Company company)
    {
        _companies.Remove(company);
    }

    public async Task<IPagedList<CompanyItem>> Find(CompanyParameters parameters)
    {
        var query = _companies.AsNoTracking();

        query = Filter(parameters, query);

        query = Order(parameters, query);

        var total = await query.CountAsync();

        var items = await query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .Select(c => new CompanyItem
            {
                Id = c.Id,
                Name = c.Name,
                Cnpj = c.Cnpj.Number
            })
            .ToListAsync();

        return new PagedList<CompanyItem>(items, total, parameters.Page, parameters.PageSize);
    }

    public async Task<bool> AnyById(Guid companyId)
    {
        return await _companies
            .AsNoTracking()
            .AnyAsync(company => company.Id == companyId);
    }

    public async Task<IEnumerable<CompanyPartnerModel>> GetPartners(Guid companyId)
    {
        return await _companyPartners
            .AsNoTracking()
            .Where(cp => cp.CompanyId == companyId)
            .Select(cp => new CompanyPartnerModel
            {
                PartnerId = cp.PartnerId,
                QualificationId = cp.QualificationId,
                JoinedAt = cp.JoinedAt.ToDateTime(TimeOnly.MinValue)
            })
            .ToListAsync();
    }

    // Private Methods

    private static IQueryable<Company> Order(
        CompanyParameters parameters, IQueryable<Company> query)
    {
        query = parameters.OrderBy switch
        {
            "name-asc" => query.OrderBy(u => u.Name),
            "name-desc" => query.OrderByDescending(u => u.Name),

            _ => query.OrderBy(u => u.Name),
        };
        return query;
    }

    private static IQueryable<Company> Filter(
        CompanyParameters parameters, IQueryable<Company> query)
    {
        if (parameters.Query is not null)
        {
            query = query.Where(p =>
                p.Name.StartsWith(parameters.Query) ||
                p.Cnpj.Number.StartsWith(parameters.Query)
            );
        }

        return query;
    }
}
