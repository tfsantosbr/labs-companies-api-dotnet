using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Enums;

using FluentValidation;

namespace Companies.Domain.Tests.Features.Companies;

public class CompanyTests
{
    [Fact(DisplayName = "Deve gerar um exception quando criar uma empresa sem um sócio")]
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

        var exception = Record.Exception(createCompanyWithoutPartner) as ValidationException;

        // assert

        Assert.Contains(exception?.Errors, error =>
            error.PropertyName == "Partners" &&
            error.ErrorMessage == "The company must be created with at least one partner"
            );
    }

    [Fact(DisplayName = "Deve gerar um exception quando criar uma empresa com sócios duplicados")]
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

        var exception = Record.Exception(createCompanyWithDuplicatedPartners) as ValidationException;

        // assert

        Assert.Contains(exception?.Errors, error =>
            error.PropertyName == "Partners" &&
            error.ErrorMessage == "There are duplicate partners in the company"
            );
    }

    [Fact(DisplayName = "Deve gerar um exception quando criar uma empresa com telefones duplicados")]
    public void ShouldThowExceptionWhenCreatingCompanyWithDuplicatedPhones()
    {
        // arrange

        void createCompanyWithDuplicatedPhones()
        {
            var partners = new[]
            {
                new CompanyPartner(new Guid("b780a07e-56e9-4750-9749-c9164df51ce5"), 1, new DateOnly(2022,1,1))
            };

            var phones = new[]
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
                phones: phones
            );
        }

        // act

        var exception = Record.Exception(createCompanyWithDuplicatedPhones) as ValidationException;

        // assert

        Assert.Contains(exception?.Errors, error =>
            error.PropertyName == "Phones" &&
            error.ErrorMessage == "There are duplicate phones in the company"
            );
    }

    [Fact(DisplayName = "Deve criar uma empresa com sucesso quando for informado dados válidos")]
    public void ShouldCreateCompanyWithSuccessWhenValidDataIsProvided()
    {
        // arrange

        void createCompanyWIthValidData()
        {
            var partners = new[]
            {
                new CompanyPartner(new Guid("6c65317c-24bf-49b0-9d80-6ccf1c06658d"), 54, new DateOnly(2022,1,1)),
                new CompanyPartner(new Guid("0016668e-3e63-4565-8b78-577e47f8482d"), 54, new DateOnly(2022,1,1)),
                new CompanyPartner(new Guid("7924572b-830b-4f7e-8b3d-e5cea7dd5c25"), 54, new DateOnly(2022,1,1)),
            };

            var phones = new[]
            {
                new CompanyPhone(new Phone("11","999999999")),
                new CompanyPhone(new Phone("11","988888888")),
                new CompanyPhone(new Phone("11","977777777")),
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
                phones: phones
            );
        }

        // act

        var exception = Record.Exception(createCompanyWIthValidData) as ValidationException;

        // assert

        Assert.Null(exception);
    }
}
