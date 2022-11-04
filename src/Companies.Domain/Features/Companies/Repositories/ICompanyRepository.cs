namespace Companies.Domain.Features.Companies.Repositories;

public interface ICompanyRepository
{
    Task Add(Company company);
    Task<bool> AnyByName(string name, Guid? ignoredId = null);
    Task<bool> AnyByCnpj(string cnpj, Guid? ignoredId = null);
    Task<Company?> GetById(Guid companyId);
}
