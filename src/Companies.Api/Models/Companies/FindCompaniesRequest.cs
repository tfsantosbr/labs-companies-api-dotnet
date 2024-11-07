namespace Companies.Api.Models.Companies;

public record FindCompaniesRequest(
    string? Name,
    string? Cnpj,
    int? Page,
    int? PageSize,
    string? OrderBy
    );
