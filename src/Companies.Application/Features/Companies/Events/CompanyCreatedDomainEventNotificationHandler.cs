using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Features.Companies.Events;

public class CompanyCreatedDomainEventNotificationHandler(
    ILogger<CompanyCreatedDomainEventNotificationHandler> logger) : INotificationHandler<CompanyCreatedDomainEvent>
{
    public Task Handle(CompanyCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Company '{CompanyName}' created: Sending E-mail...", notification.CompanyName);
        logger.LogInformation("Company '{CompanyName}' created: E-mail sent!", notification.CompanyName);

        return Task.CompletedTask;
    }
}