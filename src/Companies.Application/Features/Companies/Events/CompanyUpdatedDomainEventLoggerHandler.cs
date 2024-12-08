using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Features.Companies.Events;

public class CompanyUpdatedDomainEventLoggerHandler(
    ILogger<CompanyUpdatedDomainEventLoggerHandler> logger) : INotificationHandler<CompanyUpdatedDomainEvent>
{
    public Task Handle(CompanyUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Company updated: {CompanyId} - {CompanyName}",
            notification.CompanyId,
            notification.CompanyName
            );

        return Task.CompletedTask;
    }
}
