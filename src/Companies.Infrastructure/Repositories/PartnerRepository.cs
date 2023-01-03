using Companies.Domain.Features.Partners;
using Companies.Domain.Features.Partners.Models;
using Companies.Domain.Features.Partners.Repositories;
using Companies.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Repositories;

public class PartnerRepository : IPartnerRepository
{
    // Fields

    private readonly CompaniesContext _context;

    // Constructors

    public PartnerRepository(CompaniesContext context)
    {
        _context = context;
    }

    // Implementations

    public async Task Add(Partner partner)
    {
        await _context.Set<Partner>().AddAsync(partner);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyPartnerById(Guid partnerId)
    {
        return await _context.Set<Partner>()
            .AsNoTracking()
            .AnyAsync(p => p.Id == partnerId);
    }

    public async Task<Partner?> GetById(Guid partnerId)
    {
        return await _context.Set<Partner>()
            .FirstOrDefaultAsync(p => p.Id == partnerId);
    }

    public async Task<bool> IsDuplicatedEmail(string email)
    {
        return await _context
            .Set<Partner>()
            .AsNoTracking()
            .AnyAsync(p => p.Email.Address == email);
    }

    public async Task<IEnumerable<PartnerItem>> List()
    {
        return await _context.Set<Partner>()
            .AsNoTracking()
            .Select(u => new PartnerItem
            {
                Id = u.Id,
                Name = u.CompleteName.ToString(),
                Email = u.Email.ToString()
            })
            .ToListAsync();
    }

    public async Task Remove(Partner partner)
    {
        _context.Set<Partner>().Remove(partner);
        await _context.SaveChangesAsync();
    }
}
