using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Results;
using Companies.Application.Features.Partners.Contants;
using Companies.Application.Features.Partners.Repositories;

namespace Companies.Application.Features.Partners.Command.RemovePartner;

public class RemovePartnerCommandHandler(IPartnerRepository partnerRepository, IUnitOfWork unitOfWork)
    : AbstractHandler, ICommandHandler<RemovePartnerCommand>
{
    public async Task<Result> HandleAsync(RemovePartnerCommand command, CancellationToken cancellationToken = default)
    {
        var partner = await partnerRepository.GetByIdAsync(command.PartnerId, cancellationToken);

        if (partner == null)
            return Result.NotFound(PartnerErrors.PartnerNotFound(command.PartnerId));

        partnerRepository.Remove(partner);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
