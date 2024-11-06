namespace Companies.Application.Features.Companies.Commands;

public class RemovePartnerFromCompany
{
    public RemovePartnerFromCompany(Guid companyId, Guid partnerId)
    {
        CompanyId = companyId;
        PartnerId = partnerId;
    }

    public Guid CompanyId { get; set; }
    public Guid PartnerId { get; set; }
}
