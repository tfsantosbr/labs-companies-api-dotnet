namespace Companies.Domain.Features.Companies.Commands;

public class RemoveCompany
{
    public RemoveCompany(Guid companyId)
    {
        CompanyId = companyId;
    }

    public Guid CompanyId { get; set; }
}
