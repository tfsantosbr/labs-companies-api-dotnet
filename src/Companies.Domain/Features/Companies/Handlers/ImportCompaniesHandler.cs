using Companies.Domain.Base.Handlers;
using Companies.Domain.Base.Models;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Commands.Validators;

namespace Companies.Domain.Features.Companies.Handlers;

public class ImportCompaniesHandler : CommandHandler, IHandler<ImportCompanies, Response>
{
    // Implementations

    public async Task<Response> Handle(ImportCompanies request, CancellationToken cancellationToken = default)
    {
        if (IsInvalidRequest(request, out var notifications))
            return RequestErrorsResponse(notifications);

        foreach (var company in request.CompaniesToBeImported)
        {
            // create company message

            // send to message broker
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
