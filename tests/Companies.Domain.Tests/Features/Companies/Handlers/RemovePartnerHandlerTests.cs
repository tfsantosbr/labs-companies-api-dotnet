using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Handlers;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Domain.Tests.Features.Companies.Handlers;

public class RemovePartnerHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePartnerHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenRemovePartnerInCompanyThatDoesntExist()
    {
        // arrange

        var command = new RemovePartner();

        var handler = new RemovePartnerHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.Contains(response.Notifications, notification => notification.Message == "Company not found");
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenRemovePartnerThatDoesntExist()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetCompanyWithPartnersById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        var command = new RemovePartner();

        var handler = new RemovePartnerHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.Contains(response.Notifications, notification => notification.Message == "Partner not found");
    }

    [Fact]
    public async Task ShouldRemovePartnerWithSuccessWhenAExistingPartnerIsProvided()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetCompanyWithPartnersById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        var command = new RemovePartner
        {
            PartnerId = new Guid("6c65317c-24bf-49b0-9d80-6ccf1c06658d"),
            CompanyId = new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73")
        };

        var handler = new RemovePartnerHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.False(response.HasNotifications);
    }
}
