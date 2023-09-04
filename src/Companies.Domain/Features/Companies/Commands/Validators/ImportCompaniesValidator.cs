using FluentValidation;

namespace Companies.Domain.Features.Companies.Commands.Validators;

public class ImportCompaniesValidator : AbstractValidator<ImportCompanies>
{
    public ImportCompaniesValidator()
    {
        RuleFor(p => p.CompaniesToBeImported).NotEmpty();
    }
}
