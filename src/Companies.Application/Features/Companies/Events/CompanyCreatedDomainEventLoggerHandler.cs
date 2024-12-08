using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Features.Companies.Events;

public class CompanyCreatedDomainEventLoggerHandler(
    ILogger<CompanyCreatedDomainEventLoggerHandler> logger) : INotificationHandler<CompanyCreatedDomainEvent>
{
    public Task Handle(CompanyCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Company created: {CompanyId} - {CompanyName}",
            notification.CompanyId,
            notification.CompanyName
            );

        return Task.CompletedTask;
    }
}
