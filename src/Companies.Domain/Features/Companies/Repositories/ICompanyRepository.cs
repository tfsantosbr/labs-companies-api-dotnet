using Companies.Domain.Base.Pagination;
using Companies.Domain.Features.Companies.Models;

namespace Companies.Domain.Features.Companies.Repositories;

public interface ICompanyRepository
{
    Task Add(Company company);
    Task<bool> AnyByName(string name, Guid? ignoredId = null);
    Task<bool> AnyByCnpj(string cnpj, Guid? ignoredId = null);
    Task<bool> AnyById(Guid companyId);
    Task<Company?> GetById(Guid companyId);
    void Remove(Company company);
    Task<bool> AnyPartnerById(Guid partnerId);
    Task<IPagedList<CompanyItem>> Find(CompanyParameters parameters);
    Task<IEnumerable<CompanyPartnerModel>> GetPartners(Guid companyId);
}
