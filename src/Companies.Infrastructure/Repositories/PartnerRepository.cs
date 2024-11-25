using Companies.Application.Features.Partners;
using Companies.Application.Features.Partners.Repositories;
using Companies.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class PartnerRepository(CompaniesContext context) : IPartnerRepository
{
    public async Task AddAsync(Partner partner, CancellationToken cancellationToken = default)
    {
        await context.Partners.AddAsync(partner, cancellationToken);
    }

    public async Task<bool> AnyPartnerByIdAsync(Guid partnerId, CancellationToken cancellationToken = default)
    {
        return await context.Partners.AsNoTracking()
            .AnyAsync(p => p.Id == partnerId, cancellationToken);
    }

    public async Task<Partner?> GetByIdAsync(Guid partnerId, CancellationToken cancellationToken = default)
    {
        return await context.Partners
            .FirstOrDefaultAsync(p => p.Id == partnerId, cancellationToken);
    }

    public async Task<bool> IsDuplicatedEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await context.Partners.AsNoTracking()
            .AnyAsync(p => p.Email.Address == email, cancellationToken: cancellationToken);
    }

    public void Remove(Partner partner)
    {
        context.Partners.Remove(partner);
    }
}
