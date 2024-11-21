using Companies.Application.Features.Partners.Models;

namespace Companies.Application.Features.Partners.Repositories;

public interface IPartnerRepository
{
    Task<bool> AnyPartnerById(Guid partnerId);
    Task Add(Partner partner);
    Task<Partner?> GetById(Guid partnerId);
    Task Remove(Partner partner);
    Task<bool> IsDuplicatedEmail(string email);
}
