using FluentValidation;

namespace Companies.Application.Features.Partners.Command.CreatePartner;

public class CreatePartnerCommandValidator : AbstractValidator<CreatePartnerCommand>
{
    public CreatePartnerCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty().MaximumLength(300);
        RuleFor(c => c.LastName).NotEmpty().MaximumLength(300);
        RuleFor(c => c.Email).NotEmpty().EmailAddress();
    }
}
