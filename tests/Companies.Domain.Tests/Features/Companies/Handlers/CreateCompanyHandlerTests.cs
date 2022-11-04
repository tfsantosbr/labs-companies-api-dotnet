using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Handlers;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Domain.Tests.Features.Companies.Handlers;

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
        Assert.Contains(response.Notifications, n => n.Code == "Cnpj");
        Assert.Contains(response.Notifications, n => n.Code == "Name");
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenCreateCompanyWithDuplicatedCnpj()
    {
        // arrange
        
        _companyRepository.AnyByCnpj(Arg.Any<string>()).ReturnsForAnyArgs(Task.FromResult(true));

        var command = CompanyHelper.CreateValidCompanyCommand();

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

        var command = CompanyHelper.CreateValidCompanyCommand();

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
        
        var command = CompanyHelper.CreateValidCompanyCommand();

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
