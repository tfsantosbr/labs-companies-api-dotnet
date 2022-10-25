using FluentValidation;

namespace Companies.Domain.Features.Companies.Validations;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(p => p.Partners)
            .NotEmpty()
            .WithMessage("The company must be created with at least one partner")
            .Must(partners => !partners.GroupBy(p => p.PartnerId).Any(g => g.Count() > 1))
            .WithMessage("There are duplicate partners in the company")
            ;
        
        RuleFor(p => p.Phones)
            .Must(phones => 
                !phones.GroupBy(p => new { p.Phone.CountryCode, p.Phone.Number })
                    .Any(g => g.Count() > 1)
                )
            .WithMessage("There are duplicate phones in the company")
            ;
    }
}
