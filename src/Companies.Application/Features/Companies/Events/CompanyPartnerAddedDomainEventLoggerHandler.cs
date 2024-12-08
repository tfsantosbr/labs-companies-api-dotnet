using MediatR;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Features.Companies.Events;

public class CompanyPartnerAddedDomainEventLoggerHandler(
    ILogger<CompanyPartnerAddedDomainEventLoggerHandler> logger) : INotificationHandler<CompanyPartnerAddedDomainEvent>
{
    public Task Handle(CompanyPartnerAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Partner '{PartnerId}' added in company '{CompanyId}'",
            notification.PartnerId,
            notification.CompanyId
            );

        return Task.CompletedTask;
    }
}
