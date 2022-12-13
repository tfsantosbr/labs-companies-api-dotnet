namespace Companies.Domain.Features.Partners.Repositories;

public interface IPartnerRepository
{
    Task<bool> AnyPartnerById(Guid partnerId);
}
