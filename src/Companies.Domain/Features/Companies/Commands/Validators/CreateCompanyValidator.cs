using FluentValidation;

namespace Companies.Domain.Features.Companies.Commands.Validators;

public class CreateCompanyValidator : AbstractValidator<CreateCompany>
{
    public CreateCompanyValidator()
    {
        RuleFor(p => p.Cnpj).NotEmpty().MaximumLength(14);
        RuleFor(p => p.Name).NotEmpty().MaximumLength(500);

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
