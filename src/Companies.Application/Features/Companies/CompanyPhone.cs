using Companies.Application.Abstractions.ValueObjects;

namespace Companies.Application.Features.Companies;

public class CompanyPhone
{
    // Constructors

    public CompanyPhone(Guid companyId, Phone phone, Guid id)
    {
        Id = id;
        CompanyId = companyId;
        Phone = phone;
    }

    private CompanyPhone()
    {
    }

    // Properties

    public Guid Id { get; private set; }
    public Guid CompanyId { get; private set; }
    public Phone Phone { get; private set; } = default!;
}
