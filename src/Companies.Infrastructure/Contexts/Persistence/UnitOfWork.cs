using Companies.Application.Abstractions.Persistence;

namespace Companies.Infrastructure.Contexts.Persistence;

public class UnitOfWork(CompaniesContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
