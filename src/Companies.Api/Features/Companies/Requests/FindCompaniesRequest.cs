namespace Companies.Api.Features.Companies.Requests;

public record FindCompaniesRequest(
    string? Name,
    string? Cnpj,
    int? Page,
    int? PageSize,
    string? OrderBy
    );
