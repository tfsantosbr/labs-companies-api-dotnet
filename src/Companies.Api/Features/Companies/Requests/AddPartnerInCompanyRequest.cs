namespace Companies.Api.Features.Companies.Requests;

public record AddPartnerInCompanyRequest(Guid PartnerId, int QualificationId, DateTime JoinedAt);
