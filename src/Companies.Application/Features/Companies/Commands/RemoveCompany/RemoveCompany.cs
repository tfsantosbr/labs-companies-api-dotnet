namespace Companies.Application.Features.Companies.Commands.RemoveCompany;

public class RemoveCompany
{
    public RemoveCompany(Guid companyId)
    {
        CompanyId = companyId;
    }

    public Guid CompanyId { get; set; }
}
