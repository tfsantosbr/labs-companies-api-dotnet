using Companies.Application.Base.Persistence;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Handlers;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Application.Tests.Features.Companies.Handlers;

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

        var command = new RemovePartnerFromCompany(Guid.NewGuid(), Guid.NewGuid());

        var handler = new RemovePartnerFromCompanyHandler(
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

        _companyRepository.GetById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        var command = new RemovePartnerFromCompany(Guid.NewGuid(), Guid.NewGuid());

        var handler = new RemovePartnerFromCompanyHandler(
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

        _companyRepository.GetById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        var command = new RemovePartnerFromCompany(
           companyId: new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
           partnerId: new Guid("6c65317c-24bf-49b0-9d80-6ccf1c06658d")
        );

        var handler = new RemovePartnerFromCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.False(response.HasNotifications);
    }
}
