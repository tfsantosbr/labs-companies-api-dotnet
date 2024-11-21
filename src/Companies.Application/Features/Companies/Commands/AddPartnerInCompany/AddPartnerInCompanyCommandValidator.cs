using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.AddPartnerInCompany;

public class AddPartnerInCompanyCommandValidator : AbstractValidator<AddPartnerInCompanyCommand>
{
    public AddPartnerInCompanyCommandValidator()
    {
        RuleFor(cp => cp.CompanyId).NotEmpty();
        RuleFor(cp => cp.PartnerId).NotEmpty();
        RuleFor(cp => cp.JoinedAt).NotEmpty();
        RuleFor(cp => cp.QualificationId).NotEmpty();
    }
}
