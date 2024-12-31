using Companies.Application.Abstractions.Models;

namespace Companies.Application.Features.Companies.Models;

public record CompanyPhoneModel(Guid Id, PhoneModel Phone)
{
    public static CompanyPhoneModel FromCompanyPhone(CompanyPhone companyPhone) =>
        new(companyPhone.Id, PhoneModel.FromPhone(companyPhone.Phone));
}
