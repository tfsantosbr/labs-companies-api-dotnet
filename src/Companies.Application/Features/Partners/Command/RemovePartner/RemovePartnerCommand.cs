using Companies.Application.Abstractions.Handlers;

namespace Companies.Application.Features.Partners.Command.RemovePartner;

public record RemovePartnerCommand(Guid PartnerId) : ICommand;
