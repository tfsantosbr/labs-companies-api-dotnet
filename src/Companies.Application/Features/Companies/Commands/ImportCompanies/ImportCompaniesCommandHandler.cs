using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Messaging;
using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.Results;

namespace Companies.Application.Features.Companies.Commands.ImportCompanies;

public class ImportCompaniesCommandHandler : CommandHandler, IHandler<ImportCompaniesCommand, Response>
{
    //private fields

    private readonly IMessageBroker _messageBroker;

    // constructors

    public ImportCompaniesCommandHandler(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    // Implementations

    public async Task<Response> Handle(ImportCompaniesCommand request, CancellationToken cancellationToken = default)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        foreach (var company in request.Companies)
        {
            await _messageBroker.PostMessage(company);
        }

        return Response.Ok();
    }

    // Private Methods

    private bool IsInvalidRequest(ImportCompaniesCommand request, out IEnumerable<Notification> notifications)
    {
        var validator = new ImportCompaniesCommandValidator();
        var result = validator.Validate(request);

        notifications = result.Errors.Select(e =>
            new Notification(e.PropertyName, e.ErrorMessage)
        );

        return !result.IsValid;
    }
}
