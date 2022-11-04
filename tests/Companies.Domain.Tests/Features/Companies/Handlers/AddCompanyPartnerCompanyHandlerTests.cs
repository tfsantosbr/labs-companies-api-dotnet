using Companies.Domain.Base.Persistence;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Commands;
using Companies.Domain.Features.Companies.Handlers;
using Companies.Domain.Features.Companies.Repositories;
using Companies.Domain.Tests.Base.Helpers;

using NSubstitute;

namespace Companies.Domain.Tests.Features.Companies.Handlers;

public class AddCompanyPartnerCompanyHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCompanyPartnerCompanyHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddCompanyPartnerWithInvalidData()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddCompanyPartnerInCompanyThatDoesntExist()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public async Task ShouldReturnErrorResponseWhenAddCompanyPartnerThatIsAlreadyLinkedInTheCompany()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public async Task ShouldAddCompanyPartnerWithSuccessWhenValidPartnerIsProvided()
    {
        // arrange

        // act

        // assert
    }
}
