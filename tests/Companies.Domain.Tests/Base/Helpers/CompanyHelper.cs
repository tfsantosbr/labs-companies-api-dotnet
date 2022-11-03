using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.Companies;
using Companies.Domain.Features.Companies.Enums;

namespace Companies.Domain.Tests.Base.Helpers;

public class CompanyHelper
{
    public static Company CreateValidCompany()
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

        var company = new Company(
            id: new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
            cnpj: new Cnpj("01244660000180"),
            name: "TF Santos Informática",
            legalNature: CompanyLegalNatureType.EIRELI,
            mainActivityId: 4781400,
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
            partners: partners,
            phones: phones
        );

        return company;
    }
}
