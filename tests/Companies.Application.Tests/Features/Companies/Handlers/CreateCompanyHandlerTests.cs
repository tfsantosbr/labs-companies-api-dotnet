using Companies.Application.Abstractions.Persistence;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Application.Tests.Features.Companies.Handlers;

public class CreateCompanyHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCompanyHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenCreateCompanyWithInvalidData()
    {
        // arrange

        var command = new CreateCompany();

        var handler = new CreateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.True(response.HasNotifications);
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenCreateCompanyWithDuplicatedCnpj()
    {
        // arrange

        _companyRepository.AnyByCnpj(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(true));

        var command = CompanyHelper.GenerateValidCreateCompanyCommand();

        var handler = new CreateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.True(response.HasNotifications);
        Assert.Contains(response.Notifications, notification =>
            notification.Message.Contains("cnpj") &&
            notification.Message.Contains("already exists")
            );
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenCreateCompanyWithDuplicatedName()
    {
        // arrange

        _companyRepository.AnyByName(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(true));

        var command = CompanyHelper.GenerateValidCreateCompanyCommand();

        var handler = new CreateCompanyHandler(
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
    public async Task ShouldCreateCompanyWithSuccessWhenValidDataIsProvided()
    {
        // arrange

        var command = CompanyHelper.GenerateValidCreateCompanyCommand();

        var handler = new CreateCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.False(response.HasNotifications);
    }
}
