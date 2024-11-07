using Companies.Application.Abstractions.Persistence;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Application.Tests.Features.Companies.Handlers;

public class UpdateCompanyHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCompanyHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenUpdateCompanyWithInvalidData()
    {
        // arrange

        var command = new UpdateCompany();

        var handler = new UpdateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.True(response.HasNotifications);
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenUpdateCompanyThatDoesntExist()
    {
        // arrange

        var command = CompanyHelper.GenerateValidUpdateCompanyCommand();

        var handler = new UpdateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.True(response.HasNotifications);
        Assert.Contains(response.Notifications, notification =>
            notification.Message.Contains("Company not found")
            );
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenUpdateCompanyWithDuplicatedName()
    {
        // arrange

        _companyRepository.AnyByName(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(true));

        var command = CompanyHelper.GenerateValidUpdateCompanyCommand();

        var handler = new UpdateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.True(response.HasNotifications);
        Assert.Contains(response.Notifications, notification =>
            notification.Message.Contains("name") &&
            notification.Message.Contains("already exists")
            );
    }

    [Fact]
    public async Task ShouldUpdateCompanyWithSuccessWhenValidDataIsProvided()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetById(Arg.Any<Guid>()).ReturnsForAnyArgs(Task.FromResult<Company?>(company));

        var command = CompanyHelper.GenerateValidUpdateCompanyCommand();

        var handler = new UpdateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.False(response.HasNotifications);
    }
}
