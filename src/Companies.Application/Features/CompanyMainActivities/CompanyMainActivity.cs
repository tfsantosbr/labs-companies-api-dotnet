using Companies.Application.Abstractions.Results;

namespace Companies.Application.Features.CompanyMainActivities;

public class CompanyMainActivity
{
    private CompanyMainActivity()
    {
    }

    public int Code { get; private set; }
    public string Description { get; private set; } = default!;

    public static Result<CompanyMainActivity> Create(int code, string description)
    {
        var companyMainActivity = new CompanyMainActivity
        {
            Code = code,
            Description = description
        };

        return Result.Success(companyMainActivity);
    }
}
