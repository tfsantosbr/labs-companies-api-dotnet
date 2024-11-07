namespace Companies.Application.Features.Companies.Commands.RemoveCompany;

public class RemoveCompanyCommand
{
    public RemoveCompanyCommand(Guid companyId)
    {
        CompanyId = companyId;
    }

    public Guid CompanyId { get; set; }
}
