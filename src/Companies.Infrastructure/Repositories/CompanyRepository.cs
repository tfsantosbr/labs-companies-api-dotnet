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

    public Task<bool> AnyByCnpj(string cnpj)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyByName(string name)
    {
        throw new NotImplementedException();
    }
}
