namespace Companies.Application.Features.CompanyMainActivities;

public class CompanyMainActivity
{
    public CompanyMainActivity(int code, string description)
    {
        Code = code;
        Description = description;
    }

    private CompanyMainActivity()
    {
    }

    public int Code { get; private set; }
    public string Description { get; private set; } = default!;
}
