namespace Companies.Application.Features.Companies.Models;

public record CompanyPartnerModel(Guid PartnerId, int QualificationId, DateTime JoinedAt)
{
    public static CompanyPartnerModel FromCompanyPartner(CompanyPartner companyPartner) =>
        new(companyPartner.PartnerId, companyPartner.QualificationId, companyPartner.JoinedAt.ToDateTime(TimeOnly.MinValue));
}
