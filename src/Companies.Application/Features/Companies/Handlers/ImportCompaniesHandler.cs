using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Messaging;
using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Commands.Validators;

namespace Companies.Application.Features.Companies.Handlers;

public class ImportCompaniesHandler : CommandHandler, IHandler<ImportCompanies, Response>
{
    //private fields

    private readonly IMessageBroker _messageBroker;

    // constructors

    public ImportCompaniesHandler(IMessageBroker messageBroker)
    {
        _messageBroker = messageBroker;
    }

    // Implementations

    public async Task<Response> Handle(ImportCompanies request, CancellationToken cancellationToken = default)
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

    private bool IsInvalidRequest(ImportCompanies request, out IEnumerable<Notification> notifications)
    {
        var validator = new ImportCompaniesValidator();
        var result = validator.Validate(request);

        notifications = result.Errors.Select(e =>
            new Notification(e.PropertyName, e.ErrorMessage)
        );

        return !result.IsValid;
    }
}
