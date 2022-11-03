using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Enums;
using Companies.Domain.Tests.Base.Helpers;

namespace Companies.Domain.Tests.Features.Companies;

public class CreateCompanyTests
{
    [Fact]
    public void ShouldThowExceptionWhenCreatingCompanyWithoutPartner()
    {
        // arrange

        void createCompanyWithoutPartner() => new Company(
            id: new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
            cnpj: new Cnpj("01244660000180"),
            name: "TF Santos Informática",
            legalNature: CompanyLegalNatureType.EIRELI,
            mainActivityId: 4781400,
            address: new Address(
                postalCode: "",
                street: "",
                number: "",
                complement: null,
                neighborhood: "Residencial Oliveira",
                city: "Campo Grande",
                state: "MS",
                country: "Brasil"
            ),
            partners: new CompanyPartner[] { },
            phones: null
        );

        // act

        var exception = Record.Exception(createCompanyWithoutPartner);

        // assert

        Assert.Contains(exception.Message, "The company must be created with at least one partner");
    }

    [Fact]
    public void ShouldThowExceptionWhenCreatingCompanyWithDuplicatedPartners()
    {
        // arrange

        void createCompanyWithDuplicatedPartners()
        {
            var partners = new[]
            {
                new CompanyPartner(new Guid("b780a07e-56e9-4750-9749-c9164df51ce5"), 1, new DateOnly(2022,1,1)),
                new CompanyPartner(new Guid("b780a07e-56e9-4750-9749-c9164df51ce5"), 1, new DateOnly(2022,1,1)),
            };

            new Company(
                id: new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
                cnpj: new Cnpj("01244660000180"),
                name: "TF Santos Informática",
                legalNature: CompanyLegalNatureType.EIRELI,
                mainActivityId: 4781400,
                address: new Address(
                    postalCode: "",
                    street: "",
                    number: "",
                    complement: null,
                    neighborhood: "Residencial Oliveira",
                    city: "Campo Grande",
                    state: "MS",
                    country: "Brasil"
                ),
                partners: partners,
                phones: null
            );
        }

        // act

        var exception = Record.Exception(createCompanyWithDuplicatedPartners);

        // assert

        Assert.Contains(exception.Message, "There are duplicate partners in the company");
    }

    [Fact]
    public void ShouldThowExceptionWhenCreatingCompanyWithDuplicatedPhones()
    {
        // arrange

        void createCompanyWithDuplicatedPhones()
        {
            var partners = new[]
            {
                new CompanyPartner(new Guid("b780a07e-56e9-4750-9749-c9164df51ce5"), 1, new DateOnly(2022,1,1))
            };

            var duplicatedPhones = new[]
            {
                new CompanyPhone(new Phone("11","999999999")),
                new CompanyPhone(new Phone("11","999999999"))
            };

            new Company(
                id: new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
                cnpj: new Cnpj("01244660000180"),
                name: "TF Santos Informática",
                legalNature: CompanyLegalNatureType.EIRELI,
                mainActivityId: 4781400,
                address: new Address(
                    postalCode: "",
                    street: "",
                    number: "",
                    complement: null,
                    neighborhood: "Residencial Oliveira",
                    city: "Campo Grande",
                    state: "MS",
                    country: "Brasil"
                ),
                partners: partners,
                phones: duplicatedPhones
            );
        }

        // act

        var exception = Record.Exception(createCompanyWithDuplicatedPhones);

        // assert

        Assert.Contains(exception.Message, "There are duplicate phones in the company");
    }

    [Fact]
    public void ShouldCreateCompanyWithSuccessWhenValidDataIsProvided()
    {
        // arrange

        void createCompanyWithValidData() => CompanyHelper.CreateValidCompany();

        // act

        var exception = Record.Exception(createCompanyWithValidData);

        // assert

        Assert.Null(exception);
    }
}
