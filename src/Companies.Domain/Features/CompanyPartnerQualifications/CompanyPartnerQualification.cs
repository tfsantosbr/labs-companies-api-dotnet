namespace Companies.Domain.Features.CompanyPartnerQualifications;

public class CompanyPartnerQualification
{
    public CompanyPartnerQualification(int code, string description)
    {
        Code = code;
        Description = description;
    }

    private CompanyPartnerQualification()
    {
    }

    public int Code { get; private set; }
    public string Description { get; private set; } = default!;
}
