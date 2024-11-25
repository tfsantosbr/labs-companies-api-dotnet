using Companies.Application.Features.Partners.Models;

namespace Companies.Application.Features.Partners.Repositories;

public interface IPartnerRepository
{
    Task<bool> AnyPartnerByIdAsync(Guid partnerId, CancellationToken cancellationToken = default);
    Task AddAsync(Partner partner, CancellationToken cancellationToken = default);
    Task<Partner?> GetByIdAsync(Guid partnerId, CancellationToken cancellationToken = default);
    void Remove(Partner partner);
    Task<bool> IsDuplicatedEmailAsync(string email, CancellationToken cancellationToken = default);
}
