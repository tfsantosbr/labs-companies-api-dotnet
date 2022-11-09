using Companies.Domain.Base.Models;

namespace Companies.Domain.Features.Companies.Models;

public class CompanyPhoneModel
{
    public Guid Id { get; private set; }
    public PhoneModel Phone { get; private set; } = default!;
}
