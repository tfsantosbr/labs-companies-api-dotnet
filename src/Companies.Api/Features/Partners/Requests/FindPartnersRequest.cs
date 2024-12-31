namespace Companies.Api.Features.Partners.Requests;

public record FindPartnersRequest(string? FirstName, string? LastName, int? Page, int? PageSize, string? OrderBy);