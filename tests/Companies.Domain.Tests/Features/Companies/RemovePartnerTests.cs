using Companies.Domain.Features.Companies;
using Companies.Domain.Tests.Base.Helpers;

namespace Companies.Domain.Tests.Features.Companies;

public class RemovePartnerTests
{
    [Fact]
    public void ShouldThowExceptionWhenRemovingPartnerThatDoesntExistInTheCompany()
    {
        // arrange

        var company = CompanyHelper.CreateValidCompany();
        var nonExistingPartner = new CompanyPartner(
            new Guid("030068f4-d7bf-484f-8fd0-7b001ed8831a"), 54, new DateOnly(2022, 1, 1)
        );

        // act

        var exception = Record.Exception(() => company.RemovePartner(nonExistingPartner));

        // assert

        Assert.Contains(exception.Message, "This partner not exists in this company");
    }

    [Fact]
    public void ShouldRemoveWithSuccessWhenPassingAPartnerThatExistsInTheCompany()
    {
        // arrange

        var company = CompanyHelper.CreateValidCompany();
        var existingPartner = new CompanyPartner(
            new Guid("6c65317c-24bf-49b0-9d80-6ccf1c06658d"), 54, new DateOnly(2022, 1, 1)
        );

        // act

        var exception = Record.Exception(() => company.RemovePartner(existingPartner));

        // assert

        Assert.Null(exception);
    }
}
