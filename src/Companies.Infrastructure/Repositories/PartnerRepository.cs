using Companies.Domain.Features.Partners;
using Companies.Domain.Features.Partners.Repositories;
using Companies.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class PartnerRepository : IPartnerRepository
{
    // Fields

    private readonly DbSet<Partner> _partners;

    // Constructors

    public PartnerRepository(CompaniesContext context)
    {
        _partners = context.Set<Partner>();
    }

    // Implementations

    public async Task<bool> AnyPartnerById(Guid partnerId)
    {
        return await _partners
            .AsNoTracking()
            .AnyAsync(p => p.Id == partnerId);
    }
}
