namespace Companies.Api.Models.Partners;

public record FindPartnersRequest(string? FirstName, string? LastName, int? Page, int? PageSize, string? OrderBy);