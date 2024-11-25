using Companies.Application.Abstractions.ValueObjects;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.CompanyMainActivities;
using Companies.Application.Features.CompanyPartnerQualifications;
using Companies.Application.Features.Partners;

namespace Companies.Infrastructure.Contexts;

public class CompaniesDatabaseSeed(CompaniesContext context)
{
    public void SeedData()
    {
        SeedCompanyMainActivities();
        SeedCompanyPartnerQualifications();
        SeedPartners();
        SeedCompanies();
    }

    #region [ Company Main Activities ]

    private void SeedCompanyMainActivities()
    {
        if (!context.Set<CompanyMainActivity>().Any())
        {
            var companyMainActivity1 = CompanyMainActivity.Create(4781400,
                "Comércio varejista de artigos do vestuário e acessórios").Data!;

            var companyMainActivity2 = CompanyMainActivity.Create(9492800,
                "Atividades de organizações políticas").Data!;

            var companyMainActivity3 = CompanyMainActivity.Create(5611203,
                "Lanchonetes casas de chá de sucos e similares").Data!;

            var companyMainActivity4 = CompanyMainActivity.Create(5611201,
                "Restaurantes e similares").Data!;

            var companyMainActivity5 = CompanyMainActivity.Create(9602501,
                "Cabeleireiros manicure e pedicure").Data!;

            var companyMainActivity6 = CompanyMainActivity.Create(4399103,
                "Obras de alvenaria").Data!;

            var companyMainActivity7 = CompanyMainActivity.Create(9430800,
                "Atividades de associações de defesa de direitos sociais").Data!;

            var companyMainActivity8 = CompanyMainActivity.Create(7319002,
                "Promoção de vendas").Data!;

            var companyMainActivity9 = CompanyMainActivity.Create(4723700,
                "Comércio varejista de bebidas").Data!;

            var companyMainActivity10 = CompanyMainActivity.Create(4774100,
                "Comércio varejista de artigos de óptica").Data!;


            context.Set<CompanyMainActivity>().AddRange(
                companyMainActivity1,
                companyMainActivity2,
                companyMainActivity3,
                companyMainActivity4,
                companyMainActivity5,
                companyMainActivity6,
                companyMainActivity7,
                companyMainActivity8,
                companyMainActivity9,
                companyMainActivity10
                );

            context.SaveChanges();
        }
    }

    #endregion

    #region [ Company Partner Qualifications ]

    private void SeedCompanyPartnerQualifications()
    {
        if (!context.Set<CompanyPartnerQualification>().Any())
        {
            var companyPartnerQualification1 = CompanyPartnerQualification.Create(5, "Administrador").Data!;
            var companyPartnerQualification2 = CompanyPartnerQualification.Create(10, "Diretor").Data!;
            var companyPartnerQualification3 = CompanyPartnerQualification.Create(16, "Presidente").Data!;
            var companyPartnerQualification4 = CompanyPartnerQualification.Create(22, "Sócio").Data!;
            var companyPartnerQualification5 = CompanyPartnerQualification.Create(54, "Fundador").Data!;

            context.Set<CompanyPartnerQualification>().AddRange(
                companyPartnerQualification1,
                companyPartnerQualification2,
                companyPartnerQualification3,
                companyPartnerQualification4,
                companyPartnerQualification5
                );

            context.SaveChanges();
        }
    }

    #endregion

    #region [ Partners ]

    private void SeedPartners()
    {
        if (!context.Set<Partner>().Any())
        {
            var partner1 = Partner.Create(
                new CompleteName("Tiago", "Santos"),
                new Email("tiago@email.com"),
                id: new Guid("1a0592e2-71f0-48cc-8267-0f8d75fe0a5e"));

            var partner2 = Partner.Create(
                new CompleteName("Iran", "Nunes"),
                new Email("iran@email.com"),
                id: new Guid("9132b269-ff90-402d-88d0-2f9e7fdb312f"));

            var partner3 = Partner.Create(
                new CompleteName("Bruna", "Oliveira"),
                new Email("bruna@email.com"),
                id: new Guid("bcb63995-49c2-49d2-82ae-8c06183bfd2e"));

            var partner4 = Partner.Create(
                new CompleteName("Maria", "Gorete"),
                new Email("maria@email.com"),
                id: new Guid("012af8a2-8de0-45b7-b9c7-dcfdb3b491b3"));

            var partner5 = Partner.Create(
                new CompleteName("Natalia", "Lourenço"),
                new Email("natalia@email.com"),
                id: new Guid("fd532165-d7fd-44bb-b19b-f8e2a1c2073f"));

            var partner6 = Partner.Create(
                new CompleteName("Roberto", "Justus"),
                new Email("roberto@email.com"),
                id: new Guid("650e25ab-9b3b-4aaa-9b9a-45edb4527129"));

            var partner7 = Partner.Create(
                new CompleteName("Kim", "Katagiri"),
                new Email("kim@email.com"),
                id: new Guid("a2f7cb01-6e1e-4831-ad0e-c62e77286e88"));

            var partner8 = Partner.Create(
                new CompleteName("Renan", "Oliveira"),
                new Email("renan@email.com"),
                id: new Guid("f0a4ffdb-543b-4f38-9a3d-94e973789b74"));

            var partner9 = Partner.Create(
                new CompleteName("Will", "Toshio"),
                new Email("will@email.com"),
                id: new Guid("c7baa255-6bfb-4d67-91da-fc1d858fdf00"));

            var partner10 = Partner.Create(
                new CompleteName("Carol", "Castro"),
                new Email("carol@email.com"),
                id: new Guid("cfc6daaa-f9cd-4545-bd69-fd64bf83f891"));

            context.Set<Partner>().AddRange(
                partner1,
                partner2,
                partner3,
                partner4,
                partner5,
                partner6,
                partner7,
                partner8,
                partner9,
                partner10
                );

            context.SaveChanges();
        }
    }

    #endregion

    #region [ Companies ]

    private void SeedCompanies()
    {
        if (!context.Set<Company>().Any())
        {
            var company1 = Company.Create(
                id: new Guid("b9ffc898-c3e4-4dfb-b1c6-86778f383f73"),
                cnpj: new Cnpj("01244660000180"),
                name: "TF Santos Informática",
                legalNature: CompanyLegalNatureType.EIRELI,
                mainActivityId: 4781400,
                address: new Address(
                    postalCode: "79091719",
                    street: "Avenida Prefeito Lúdio Martins Coelho",
                    number: "863",
                    complement: null,
                    neighborhood: "Residencial Oliveira",
                    city: "Campo Grande",
                    state: "MS",
                    country: "Brasil"
                )
            ).Data!;

            company1.AddPartner(new Guid("1a0592e2-71f0-48cc-8267-0f8d75fe0a5e"), 54, new DateOnly(2022, 1, 1));
            company1.AddPhone(new Phone("11", "999999999"));
            company1.AddPhone(new Phone("11", "988888888"));
            company1.AddPhone(new Phone("11", "977777777"));

            context.Set<Company>().AddRange(company1);

            context.SaveChanges();
        }
    }

    #endregion
}
