using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Messaging;
using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.Validations;

namespace Companies.Application.Features.Companies.Commands.ImportCompanies;

public class ImportCompaniesCommandHandler(IMessageBroker messageBroker, ICommandValidator<ImportCompaniesCommand> validator) 
    : CommandHandler, ICommandHandler<ImportCompaniesCommand>
{
    public async Task<Result> Handle(ImportCompaniesCommand command, CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(command);

        if (validationResult.IsFailure)
            return validationResult;

        foreach (var company in command.Companies)
            await messageBroker.PostMessageAsync(company);

        return Result.Success();
    }
}
