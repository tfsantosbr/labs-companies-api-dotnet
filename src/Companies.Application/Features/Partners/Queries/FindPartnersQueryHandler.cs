using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Pagination;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Partners.Models;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Features.Partners.Queries;

public class FindPartnersQueryHandler(ICompaniesContext context) : 
    IQueryHandler<FindPartnersQuery, IPagedList<PartnerItem>>
{
    // Implemenations

    public async Task<Result<IPagedList<PartnerItem>>> HandleAsync(
        FindPartnersQuery query, CancellationToken cancellationToken = default)
    {
        var partners = context.Partners.AsNoTracking();

        partners = Filter(query, partners);
        partners = Order(query, partners);

        var total = await partners.CountAsync(cancellationToken: cancellationToken);
        var items = await partners
            .Page(query.PageNumber, query.PageSize)
            .Select(partner => PartnerItem.FromPartner(partner))
            .ToListAsync(cancellationToken);

        var pagedItems = new PagedList<PartnerItem>(items, total, query.PageNumber, query.PageSize);

        return Result<IPagedList<PartnerItem>>.Success(pagedItems);
    }

    // Private Methods

    private static IQueryable<Partner> Order(FindPartnersQuery query, IQueryable<Partner> queryable)
    {
        queryable = query.OrderBy switch
        {
            "first-name-asc" => queryable.OrderBy(u => u.CompleteName.FirstName),
            "first-name-desc" => queryable.OrderByDescending(u => u.CompleteName.FirstName),

            _ => queryable.OrderBy(u => u.CompleteName.FirstName),
        };

        return queryable;
    }

    private static IQueryable<Partner> Filter(FindPartnersQuery query, IQueryable<Partner> queryable)
    {
        if (query.FirstName is not null)
            queryable = queryable.Where(p => p.CompleteName.FirstName.StartsWith(query.FirstName));

        if (query.LastName is not null)
            queryable = queryable.Where(p => p.CompleteName.LastName.StartsWith(query.LastName));

        return queryable;
    }
}
