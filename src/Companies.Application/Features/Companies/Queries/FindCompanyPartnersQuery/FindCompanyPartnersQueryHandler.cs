using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Features.Partners.Models;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Features.Companies.Queries.FindCompanyPartnersQuery;

public class FindCompanyPartnersQueryHandler(ICompaniesContext context, ICompanyRepository companyRepository)
    : AbstractHandler<IEnumerable<PartnerItem>>, IQueryHandler<FindCompanyPartnersQuery, IEnumerable<PartnerItem>>
{
    public async Task<Result<IEnumerable<PartnerItem>>> HandleAsync(
        FindCompanyPartnersQuery query, CancellationToken cancellationToken = default)
    {
        if (!await companyRepository.AnyByIdAsync(query.CompanyId, cancellationToken))
            return NotFoundResult(CompanyErrors.CompanyNotFound(query.CompanyId));

        var companyPartners = await context.CompanyPartners.AsNoTracking()
            .Include(cp => cp.Partner)
            .Where(cp => cp.CompanyId == query.CompanyId)
            .Select(cp => PartnerItem.FromPartner(cp.Partner))
            .ToListAsync(cancellationToken);

        return SuccessResult(companyPartners);
    }
}
