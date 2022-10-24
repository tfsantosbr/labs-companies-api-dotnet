using Companies.Domain.Base.ValueObjects;

namespace Companies.Domain.Features.Companies;

public class CompanyPhone
{
    public CompanyPhone(Phone phone, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Phone = phone;
    }

    private CompanyPhone()
    {
    }

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Phone Phone { get; private set; } = default!;
}
