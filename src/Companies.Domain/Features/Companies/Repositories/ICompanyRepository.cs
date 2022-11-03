namespace Companies.Domain.Features.Companies.Repositories;

public interface ICompanyRepository
{
    Task Add(Company company);
    Task<bool> AnyByCnpj(string cnpj);
    Task<bool> AnyByName(string name);
}
