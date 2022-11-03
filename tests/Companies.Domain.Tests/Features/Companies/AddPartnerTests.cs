using Companies.Domain.Features.Companies;
using Companies.Domain.Tests.Base.Helpers;

namespace Companies.Domain.Tests.Features.Companies;

public class AddPartnerTests
{
    [Fact]
    public void ShouldThowExceptionWhenAddDuplicatedPartner()
    {
        // arrange

        var company = CompanyHelper.CreateValidCompany();
        var duplicatedPartner = new CompanyPartner(
            new Guid("6c65317c-24bf-49b0-9d80-6ccf1c06658d"), 54, new DateOnly(2022, 1, 1)
        );

        // act

        var exception = Record.Exception(() => company.AddPartner(duplicatedPartner));

        // assert

        Assert.Contains(exception.Message, "This partner is already linked with the company");
    }

    [Fact]
    public void ShouldAddWithSuccessWhenProvidedAValidPartner()
    {
        // arrange

        var company = CompanyHelper.CreateValidCompany();
        var companyPartner = new CompanyPartner(
            new Guid("fe092825-b7b6-4bc7-9a42-ec56559c119a"), 54, new DateOnly(2022, 1, 1)
        );

        // act

        var exception = Record.Exception(() => company.AddPartner(companyPartner));

        // assert

        Assert.Null(exception);
    }
}
