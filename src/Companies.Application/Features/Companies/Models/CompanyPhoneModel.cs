using Companies.Application.Base.Models;

namespace Companies.Application.Features.Companies.Models;

public class CompanyPhoneModel
{
    public Guid Id { get; private set; }
    public PhoneModel Phone { get; private set; } = default!;
}
