using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Handlers;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Domain.Tests.Features.Companies.Handlers;

public class RemoveCompanyHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCompanyHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenRemoveCompanyThatDoesntExist()
    {
        // arrange

        var command = new RemoveCompany(Guid.NewGuid());

        var handler = new RemoveCompanyHandler(
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
    public async Task ShouldRemoveCompanyWithSuccessWhenRemoveCompanyThatExist()
    {
        // arrange
        
        var company = CompanyHelper.GenerateValidCompany();

        _companyRepository.GetById(company.Id).Returns(Task.FromResult<Company?>(company));

        var command = new RemoveCompany(company.Id);

        var handler = new RemoveCompanyHandler(
            companyRepository: _companyRepository,
            unitOfWork: _unitOfWork
        );

        // act

        var response = await handler.Handle(command, new CancellationToken());

        // assert

        Assert.False(response.HasNotifications);
    }
}
