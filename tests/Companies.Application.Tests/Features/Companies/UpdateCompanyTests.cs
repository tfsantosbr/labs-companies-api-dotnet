using Companies.Application.Base.ValueObjects;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Tests.Base.Helpers;

namespace Companies.Application.Tests.Features.Companies;

public class UpdateCompanyTests
{
    [Fact]
    public void ShouldThowExceptionWhenUpdatingCompanyWithDuplicatedPhones()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();
        var duplicatedPhones = new[]
        {
            new CompanyPhone(new Phone("11","999999999")),
            new CompanyPhone(new Phone("11","999999999"))
        };

        // act

        var exception = Record.Exception(() => company.Update(
            name: "Updated Name",
            legalNature: CompanyLegalNatureType.LTDA,
            mainActivityId: 2,
            address: new Address(
                postalCode: "00000001",
                street: "Street Test",
                number: "1",
                complement: null,
                neighborhood: "Residencial Oliveira",
                city: "Campo Grande",
                state: "MS",
                country: "Brasil"
            ),
            phones: duplicatedPhones
        ));

        // assert

        Assert.Contains(exception.Message, "There are duplicate phones in the company");
    }

    [Fact]
    public void ShouldUpdateCompanyWithSuccessWhenValidDataIsProvided()
    {
        // arrange

        var company = CompanyHelper.GenerateValidCompany();
        var duplicatedPhones = new[]
        {
            new CompanyPhone(new Phone("11","999999999")),
            new CompanyPhone(new Phone("11","988888888"))
        };

        // act

        var exception = Record.Exception(() => company.Update(
            name: "Updated Name",
            legalNature: CompanyLegalNatureType.LTDA,
            mainActivityId: 2,
            address: new Address(
                postalCode: "00000001",
                street: "Street Test",
                number: "1",
                complement: null,
                neighborhood: "Residencial Oliveira",
                city: "Campo Grande",
                state: "MS",
                country: "Brasil"
            ),
            phones: duplicatedPhones
        ));

        // assert

        Assert.Null(exception);
    }
}
