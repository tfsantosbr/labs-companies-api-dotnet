using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Partners.Models;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Features.Partners.Queries;

public record ListPartnersQuery : IQuery<IEnumerable<PartnerItem>>;

public class ListPartnersQueryHandler(ICompaniesContext context) : 
    IQueryHandler<ListPartnersQuery, IEnumerable<PartnerItem>>
{
    public async Task<Result<IEnumerable<PartnerItem>>> Handle(
        ListPartnersQuery query, CancellationToken cancellationToken = default)
    {
        var partnersList = await context.Partners.AsNoTracking()
            .Select(u => new PartnerItem(u.Id, u.CompleteName.ToString(), u.Email.ToString()))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result<IEnumerable<PartnerItem>>.Success(partnersList);
    }
}
