namespace Companies.Application.Features.Companies.Repositories;

public interface ICompanyRepository
{
    Task AddAsync(Company company, CancellationToken cancellationToken = default);
    Task<bool> AnyByNameAsync(string name, Guid? ignoredId = null, CancellationToken cancellationToken = default);
    Task<bool> AnyByCnpjAsync(string cnpj, Guid? ignoredId = null, CancellationToken cancellationToken = default);
    Task<bool> AnyByIdAsync(Guid companyId, CancellationToken cancellationToken = default);
    Task<Company?> GetByIdAsync(Guid companyId, CancellationToken cancellationToken = default);
    void Remove(Company company);
}
