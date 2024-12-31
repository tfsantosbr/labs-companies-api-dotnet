using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Pagination;
using Companies.Application.Abstractions.Queries;
using Companies.Application.Features.Partners.Models;

namespace Companies.Application.Features.Partners.Queries;

public record FindPartnersQuery : SearchQuery, IQuery<IPagedList<PartnerItem>>
{
    public FindPartnersQuery(string? firstName, string? lastName, int? page, int? pageSize, string? orderBy)
        : base(page, pageSize, orderBy)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string? FirstName { get; }
    public string? LastName { get; }
}
