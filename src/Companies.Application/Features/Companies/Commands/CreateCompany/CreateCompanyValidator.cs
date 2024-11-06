using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.CreateCompany;

public class CreateCompanyValidator : AbstractValidator<CreateCompany>
{
    public CreateCompanyValidator()
    {
        RuleFor(p => p.Cnpj).NotEmpty().MaximumLength(14);
        RuleFor(p => p.Name).NotEmpty().MaximumLength(500);
        RuleFor(p => p.LegalNature).NotEmpty().IsInEnum();
        RuleFor(p => p.MainActivityId).NotEmpty();
        RuleFor(p => p.Address).NotEmpty();

        When(p => p.Address != null, () =>
        {
            RuleFor(p => p.Address.PostalCode).NotEmpty().MaximumLength(15);
            RuleFor(p => p.Address.Street).NotEmpty().MaximumLength(200);
            RuleFor(p => p.Address.Number).NotEmpty().MaximumLength(10);
            RuleFor(p => p.Address.Complement).MaximumLength(120);
            RuleFor(p => p.Address.Neighborhood).NotEmpty().MaximumLength(100);
            RuleFor(p => p.Address.City).NotEmpty().MaximumLength(80);
            RuleFor(p => p.Address.State).NotEmpty().MaximumLength(3);
            RuleFor(p => p.Address.Country).NotEmpty().MaximumLength(60);
        });

        RuleFor(p => p.Partners)
            .NotEmpty()
            .WithMessage("The company must be created with at least one partner")
            .Must(partners =>
                partners == null ||
                !partners.GroupBy(p => p.PartnerId).Any(g => g.Count() > 1)
            )
            .WithMessage("There are duplicate partners in the company")
            ;

        RuleFor(p => p.Phones)
            .Must(phones =>
                phones == null ||
                !phones
                    .GroupBy(p => new { p.CountryCode, p.Number })
                    .Any(g => g.Count() > 1)
            )
            .WithMessage("There are duplicate phones in the company")
            ;
    }
}
