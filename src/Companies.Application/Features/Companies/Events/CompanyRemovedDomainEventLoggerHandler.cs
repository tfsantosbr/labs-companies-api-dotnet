using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Features.Companies.Events;

public class CompanyRemovedDomainEventLoggerHandler(
    ILogger<CompanyRemovedDomainEventLoggerHandler> logger) : INotificationHandler<CompanyRemovedDomainEvent>
{
    public Task Handle(CompanyRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Company removed: {CompanyId} - {CompanyName}",
            notification.CompanyId,
            notification.CompanyName
            );

        return Task.CompletedTask;
    }
}
