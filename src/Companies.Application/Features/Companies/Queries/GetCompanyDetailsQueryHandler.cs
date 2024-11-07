using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Constants;
using Companies.Application.Features.Companies.Models;
using Microsoft.EntityFrameworkCore;

namespace Companies.Application.Features.Companies.Queries;

public class GetCompanyDetailsQueryHandler(ICompaniesContext context) : IQueryHandler<GetCompanyDetailsQuery, CompanyDetails>
{
    public async Task<Result<CompanyDetails>> Handle(GetCompanyDetailsQuery query, CancellationToken cancellationToken = default)
    {
        var companyDetails = await context.Companies.AsNoTracking()
            .Where(company => company.Id == query.CompanyId)
            .Select(company => CompanyDetails.FromCompany(company))
            .FirstOrDefaultAsync(cancellationToken);

        return companyDetails is null
            ? Result<CompanyDetails>.NotFound(CompanyErrors.CompanyNotFound(query.CompanyId))
            : Result<CompanyDetails>.Success(companyDetails);
    }
}
