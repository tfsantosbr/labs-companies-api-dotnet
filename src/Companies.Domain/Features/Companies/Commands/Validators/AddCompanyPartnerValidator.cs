using FluentValidation;

namespace Companies.Domain.Features.Companies.Commands.Validators;

public class AddCompanyPartnerValidator : AbstractValidator<AddCompanyPartner>
{
    public AddCompanyPartnerValidator()
    {
        RuleFor(cp => cp.CompanyId).NotEmpty();
        RuleFor(cp => cp.PartnerId).NotEmpty();
        RuleFor(cp => cp.JoinedAt).NotEmpty();
        RuleFor(cp => cp.QualificationId).NotEmpty();
    }
}
