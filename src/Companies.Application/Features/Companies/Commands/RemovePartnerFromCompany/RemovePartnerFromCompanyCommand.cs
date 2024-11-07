namespace Companies.Application.Features.Companies.Commands.RemovePartnerFromCompany;

public class RemovePartnerFromCompanyCommand
{
    public RemovePartnerFromCompanyCommand(Guid companyId, Guid partnerId)
    {
        CompanyId = companyId;
        PartnerId = partnerId;
    }

    public Guid CompanyId { get; set; }
    public Guid PartnerId { get; set; }
}
