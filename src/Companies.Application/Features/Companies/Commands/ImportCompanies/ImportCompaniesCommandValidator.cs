using FluentValidation;

namespace Companies.Application.Features.Companies.Commands.ImportCompanies;

public class ImportCompaniesCommandValidator : AbstractValidator<ImportCompaniesCommand>
{
    public ImportCompaniesCommandValidator()
    {
        RuleFor(p => p.Companies).NotEmpty();
    }
}
