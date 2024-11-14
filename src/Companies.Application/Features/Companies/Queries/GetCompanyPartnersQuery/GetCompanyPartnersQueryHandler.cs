using Companies.Application.Abstractions.Database;
using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Models;

using Dapper;

namespace Companies.Application.Features.Companies.Queries.GetCompanyPartnersQuery;

public class GetCompanyPartnersQueryHandler(IDapperFactory dapperFactory)
    : IQueryHandler<GetCompanyPartnersQuery, IEnumerable<CompanyPartnerModel>>
{
    public async Task<Result<IEnumerable<CompanyPartnerModel>>> Handle(
        GetCompanyPartnersQuery query, CancellationToken cancellationToken = default)
    {
        using var dbConnection = dapperFactory.CreateConnection();

        const string sql = @"
            SELECT * 
            FROM CompanyPartners 
            WHERE CompanyId = @CompanyId";

        var companyPartners = await dbConnection.QueryAsync<CompanyPartnerModel>(sql, new { query.CompanyId });

        return Result<IEnumerable<CompanyPartnerModel>>.Success(companyPartners);
    }
}