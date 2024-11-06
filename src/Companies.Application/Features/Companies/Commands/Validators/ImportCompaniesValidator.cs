using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.Validators;

public class ImportCompaniesValidator : AbstractValidator<ImportCompanies>
{
    public ImportCompaniesValidator()
    {
        RuleFor(p => p.Companies).NotEmpty();
    }
}
