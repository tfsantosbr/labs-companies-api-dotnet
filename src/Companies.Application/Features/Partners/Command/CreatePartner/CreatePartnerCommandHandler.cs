using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Partners.Contants;
using Companies.Application.Features.Partners.Models;
using Companies.Application.Features.Partners.Repositories;

namespace Companies.Application.Features.Partners.Command.CreatePartner;

public class CreatePartnerCommandHandler(IPartnerRepository repository, IUnitOfWork unitOfWork) :
    CommandHandler<PartnerDetails>, ICommandHandler<CreatePartnerCommand, PartnerDetails>
{
    // Implementation

    public async Task<Result<PartnerDetails>> HandleAsync(
        CreatePartnerCommand command, CancellationToken cancellationToken = default)
    {
        if (await IsDuplicatedEmail(command.Email, cancellationToken))
        {
            return ErrorResult(PartnerErrors.EmailAlreadyExists(command.Email));
        }

        var partner = Partner.Create(
            new CompleteName(command.FirstName, command.LastName),
            new Email(command.Email)
        );
        
        await repository.AddAsync(partner, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var partnerDetails = PartnerDetails.FromPartner(partner);

        return Result.Success(partnerDetails);
    }

    // Private Methods

    private async Task<bool> IsDuplicatedEmail(string email, CancellationToken cancellationToken)
    {
        return await repository.IsDuplicatedEmailAsync(email, cancellationToken);
    }
}