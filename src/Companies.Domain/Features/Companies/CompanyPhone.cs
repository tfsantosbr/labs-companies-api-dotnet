using Companies.Domain.Base.ValueObjects;

namespace Companies.Domain.Features.Companies;

public class CompanyPhone
{
    public CompanyPhone(Phone phone)
    {
        Phone = phone;
    }

    private CompanyPhone()
    {
    }

    public Guid CompanyId { get; private set; }
    public Phone Phone { get; private set; } = default!;
}
