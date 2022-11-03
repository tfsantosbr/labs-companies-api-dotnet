using Companies.Domain.Base.Persistence;

namespace Companies.Infrastructure.Contexts.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CompaniesContext _context;

    public UnitOfWork(CompaniesContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
