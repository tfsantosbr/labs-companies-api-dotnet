using Companies.Application.Abstractions.Handlers;
using Companies.Application.Features.Partners.Models;

namespace Companies.Application.Features.Partners.Command.CreatePartner;

public record CreatePartnerCommand(string FirstName, string LastName, string Email) : ICommand<PartnerDetails>;
