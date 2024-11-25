namespace Companies.Api.Models.Companies;

public record AddPartnerInCompanyRequest(Guid PartnerId, int QualificationId, DateTime JoinedAt);
