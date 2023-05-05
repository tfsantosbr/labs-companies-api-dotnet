using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Handlers;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Features.Partners.Repositories;
using Companies.Domain.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Domain.Tests.Features.Companies.Handlers;

public class AddPartnerHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IPartnerRepository _partnerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPartnerHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _partnerRepository = Substitute.For<IPartnerRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddPartnerWithInvalidData()
    {
        // arrange

        var command = new AddPartnerInCompany();

        var handler = new AddPartnerInCompanyHandler(
            companyRepository: _companyRepository,
            partnerRepository: _partnerRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.True(response.HasNotifications);
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddPartnerInCompanyThatDoesntExist()
    {
        // arrange

        var command = new AddPartnerInCompany
        {
            PartnerId = Guid.NewGuid(),
            CompanyId = Guid.NewGuid(),
            JoinedAt = new DateTime(2022, 1, 1),
            QualificationId = 1
        };

        var handler = new AddPartnerInCompanyHandler(
            companyRepository: _companyRepository,
            partnerRepository: _partnerRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.Contains(response.Notifications, notification => notification.Message == "Company not found");
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddPartnerThatDoesntExist()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        _partnerRepository.AnyPartnerById(Arg.Any<Guid>()).Returns(Task.FromResult(false));

        var command = new AddPartnerInCompany
        {
            PartnerId = Guid.NewGuid(),
            CompanyId = Guid.NewGuid(),
            JoinedAt = new DateTime(2022, 1, 1),
            QualificationId = 1
        };

        var handler = new AddPartnerInCompanyHandler(
            companyRepository: _companyRepository,
            partnerRepository: _partnerRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.Contains(response.Notifications, notification => notification.Message == "Partner not found");
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddPartnerThatIsAlreadyLinkedInTheCompany()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        _partnerRepository.AnyPartnerById(Arg.Any<Guid>()).Returns(Task.FromResult(true));

        var command = new AddPartnerInCompany
        {
            PartnerId = new Guid("6c65317c-24bf-49b0-9d80-6ccf1c06658d"),
            CompanyId = new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
            JoinedAt = new DateTime(2022, 1, 1),
            QualificationId = 1
        };

        var handler = new AddPartnerInCompanyHandler(
            companyRepository: _companyRepository,
            partnerRepository: _partnerRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.Contains(response.Notifications, notification =>
            notification.Message == "This partner is already linked with this company");
    }

    [Fact]
    public async Task ShouldAddPartnerWithSuccessWhenValidPartnerIsProvided()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetById(Arg.Any<Guid>())
            .Returns(Task.FromResult<Company?>(company));

        _partnerRepository.AnyPartnerById(Arg.Any<Guid>()).Returns(Task.FromResult(true));

        var command = new AddPartnerInCompany
        {
            PartnerId = Guid.NewGuid(),
            CompanyId = new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
            JoinedAt = new DateTime(2022, 1, 1),
            QualificationId = 1
        };

        var handler = new AddPartnerInCompanyHandler(
            companyRepository: _companyRepository,
            partnerRepository: _partnerRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.False(response.HasNotifications);
    }
}
