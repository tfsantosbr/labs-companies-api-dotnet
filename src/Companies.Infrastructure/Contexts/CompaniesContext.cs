using System.Reflection;
using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Domains;
using Companies.Application.Features.Companies;
using Companies.Application.Features.CompanyMainActivities;
using Companies.Application.Features.CompanyPartnerQualifications;
using Companies.Application.Features.Partners;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Contexts;

public class CompaniesContext(DbContextOptions<CompaniesContext> options, IPublisher publisher)
    : DbContext(options), ICompaniesContext
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyPartner> CompanyPartners => Set<CompanyPartner>();
    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<CompanyMainActivity> CompanyMainActivities => Set<CompanyMainActivity>();
    public DbSet<CompanyPartnerQualification> CompanyPartnerQualifications => Set<CompanyPartnerQualification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = GetDomainEvents();

        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchDomainEvents(domainEvents);

        return result;
    }

    private async Task DispatchDomainEvents(IEnumerable<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
            await publisher.Publish(domainEvent);
    }

    private List<IDomainEvent> GetDomainEvents()
    {
        return ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .SelectMany(e =>
            {
                var events = e.DomainEvents.ToList();
                e.ClearEvents();
                return events;
            })
            .ToList();
    }
}
