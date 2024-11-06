using Companies.Application.Base.ValueObjects;

namespace Companies.Application.Features.Companies;

public class CompanyPhone
{
    public CompanyPhone(Phone phone)
    {
        Phone = phone;
    }

    private CompanyPhone()
    {
    }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Phone Phone { get; private set; } = default!;
}
