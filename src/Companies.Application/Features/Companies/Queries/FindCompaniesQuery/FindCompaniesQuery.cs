using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Pagination;
using Companies.Application.Features.Companies.Models;

namespace Companies.Application.Features.Companies.Queries.FindCompaniesQuery;

public record FindCompaniesQuery : SearchQuery, IQuery<IPagedList<CompanyItem>>
{
    public FindCompaniesQuery(string? name, string? cnpj, int? page, int? pageSize, string? orderBy)
        : base(page, pageSize, orderBy)
    {
        Name = name;
        Cnpj = cnpj;
    }

    public string? Name { get; }
    public string? Cnpj { get; }
}

