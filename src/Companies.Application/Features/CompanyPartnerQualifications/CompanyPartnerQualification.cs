using Companies.Application.Abstractions.Results;

namespace Companies.Application.Features.CompanyPartnerQualifications;

public class CompanyPartnerQualification
{
    private CompanyPartnerQualification()
    {
    }

    public int Code { get; private set; }
    public string Description { get; private set; } = default!;

    public static Result<CompanyPartnerQualification> Create(int code, string description)
    {
        var companyPartnerQualification = new CompanyPartnerQualification
        {
            Code = code,
            Description = description
        };

        return Result.Success(companyPartnerQualification);
    }
}
