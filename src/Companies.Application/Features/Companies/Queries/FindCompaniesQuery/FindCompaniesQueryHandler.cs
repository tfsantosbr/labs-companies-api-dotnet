using System.Runtime.CompilerServices;
using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Pagination;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Models;

using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Features.Companies.Queries.FindCompaniesQuery;

public class FindCompaniesQueryHandler(ICompaniesContext context) : IQueryHandler<FindCompaniesQuery, IPagedList<CompanyItem>>
{
    public async Task<Result<IPagedList<CompanyItem>>> HandleAsync(
        FindCompaniesQuery query, CancellationToken cancellationToken = default)
    {
        var companies = context.Companies.AsNoTracking();

        companies = Filter(query, companies);
        companies = Order(query, companies);

        var total = await companies.CountAsync(cancellationToken: cancellationToken);
        var items = await companies
            .Page(query.PageNumber,query.PageSize)
            .Select(c => new CompanyItem
            {
                Id = c.Id,
                Name = c.Name,
                Cnpj = c.Cnpj.Number
            })
            .ToListAsync(cancellationToken: cancellationToken);

        var pagedItems = new PagedList<CompanyItem>(items, total, query.PageNumber, query.PageSize);

        return Result<IPagedList<CompanyItem>>.Success(pagedItems);
    }

    // Private Methods

    private static IQueryable<Company> Order(FindCompaniesQuery query, IQueryable<Company> queryable)
    {
        queryable = query.OrderBy switch
        {
            "name-asc" => queryable.OrderBy(u => u.Name),
            "name-desc" => queryable.OrderByDescending(u => u.Name),

            _ => queryable.OrderBy(u => u.Name),
        };

        return queryable;
    }

    private static IQueryable<Company> Filter(FindCompaniesQuery query, IQueryable<Company> queryable)
    {
        if (query.Name is not null)
            queryable = queryable.Where(p => p.Name.StartsWith(query.Name));

        if (query.Cnpj is not null)
            queryable = queryable.Where(p => p.Cnpj.Number == query.Cnpj);

        return queryable;
    }

    
}
