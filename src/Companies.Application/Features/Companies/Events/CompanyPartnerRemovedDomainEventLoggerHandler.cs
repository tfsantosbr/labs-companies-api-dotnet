using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Features.Companies.Events;

public class CompanyPartnerRemovedDomainEventLoggerHandler(
    ILogger<CompanyPartnerRemovedDomainEventLoggerHandler> logger) : INotificationHandler<CompanyPartnerRemovedDomainEvent>
{
    public Task Handle(CompanyPartnerRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Partner '{PartnerId}' removed from company '{CompanyId}'",
            notification.PartnerId,
            notification.CompanyId
            );

        return Task.CompletedTask;
    }
}
