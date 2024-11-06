using Companies.Application.Base.ValueObjects;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Enums;
using Companies.Application.Features.CompanyMainActivities;
using Companies.Application.Features.CompanyPartnerQualifications;
using Companies.Application.Features.Partners;

namespace Companies.Infrastructure.Contexts;

public class CompaniesDatabaseSeed
{
    private readonly CompaniesContext _context;

    public CompaniesDatabaseSeed(CompaniesContext context)
    {
        _context = context;
    }

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
        if (!_context.Set<CompanyMainActivity>().Any())
        {
            var companyMainActivity1 = new CompanyMainActivity(4781400,
                "Comércio varejista de artigos do vestuário e acessórios");

            var companyMainActivity2 = new CompanyMainActivity(9492800,
                "Atividades de organizações políticas");

            var companyMainActivity3 = new CompanyMainActivity(5611203,
                "Lanchonetes casas de chá de sucos e similares");

            var companyMainActivity4 = new CompanyMainActivity(5611201,
                "Restaurantes e similares");

            var companyMainActivity5 = new CompanyMainActivity(9602501,
                "Cabeleireiros manicure e pedicure");

            var companyMainActivity6 = new CompanyMainActivity(4399103,
                "Obras de alvenaria");

            var companyMainActivity7 = new CompanyMainActivity(9430800,
                "Atividades de associações de defesa de direitos sociais");

            var companyMainActivity8 = new CompanyMainActivity(7319002,
                "Promoção de vendas");

            var companyMainActivity9 = new CompanyMainActivity(4723700,
                "Comércio varejista de bebidas");

            var companyMainActivity10 = new CompanyMainActivity(4774100,
                "Comércio varejista de artigos de óptica");


            _context.Set<CompanyMainActivity>().AddRange(
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

            _context.SaveChanges();
        }
    }

    #endregion

    #region [ Company Partner Qualifications ]

    private void SeedCompanyPartnerQualifications()
    {
        if (!_context.Set<CompanyPartnerQualification>().Any())
        {
            var companyPartnerQualification1 = new CompanyPartnerQualification(5, "Administrador");
            var companyPartnerQualification2 = new CompanyPartnerQualification(10, "Diretor");
            var companyPartnerQualification3 = new CompanyPartnerQualification(16, "Presidente");
            var companyPartnerQualification4 = new CompanyPartnerQualification(22, "Sócio");
            var companyPartnerQualification5 = new CompanyPartnerQualification(54, "Fundador");

            _context.Set<CompanyPartnerQualification>().AddRange(
                companyPartnerQualification1,
                companyPartnerQualification2,
                companyPartnerQualification3,
                companyPartnerQualification4,
                companyPartnerQualification5
                );

            _context.SaveChanges();
        }
    }

    #endregion

    #region [ Partners ]

    private void SeedPartners()
    {
        if (!_context.Set<Partner>().Any())
        {
            var partner1 = new Partner(
                new CompleteName("Tiago", "Santos"),
                new Email("tiago@email.com"),
                id: new Guid("1a0592e2-71f0-48cc-8267-0f8d75fe0a5e"));

            var partner2 = new Partner(
                new CompleteName("Iran", "Nunes"),
                new Email("iran@email.com"),
                id: new Guid("9132b269-ff90-402d-88d0-2f9e7fdb312f"));

            var partner3 = new Partner(
                new CompleteName("Bruna", "Oliveira"),
                new Email("bruna@email.com"),
                id: new Guid("bcb63995-49c2-49d2-82ae-8c06183bfd2e"));

            var partner4 = new Partner(
                new CompleteName("Maria", "Gorete"),
                new Email("maria@email.com"),
                id: new Guid("012af8a2-8de0-45b7-b9c7-dcfdb3b491b3"));

            var partner5 = new Partner(
                new CompleteName("Natalia", "Lourenço"),
                new Email("natalia@email.com"),
                id: new Guid("fd532165-d7fd-44bb-b19b-f8e2a1c2073f"));

            var partner6 = new Partner(
                new CompleteName("Roberto", "Justus"),
                new Email("roberto@email.com"),
                id: new Guid("650e25ab-9b3b-4aaa-9b9a-45edb4527129"));

            var partner7 = new Partner(
                new CompleteName("Kim", "Katagiri"),
                new Email("kim@email.com"),
                id: new Guid("a2f7cb01-6e1e-4831-ad0e-c62e77286e88"));

            var partner8 = new Partner(
                new CompleteName("Renan", "Oliveira"),
                new Email("renan@email.com"),
                id: new Guid("f0a4ffdb-543b-4f38-9a3d-94e973789b74"));

            var partner9 = new Partner(
                new CompleteName("Will", "Toshio"),
                new Email("will@email.com"),
                id: new Guid("c7baa255-6bfb-4d67-91da-fc1d858fdf00"));

            var partner10 = new Partner(
                new CompleteName("Carol", "Castro"),
                new Email("carol@email.com"),
                id: new Guid("cfc6daaa-f9cd-4545-bd69-fd64bf83f891"));

            _context.Set<Partner>().AddRange(
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

            _context.SaveChanges();
        }
    }

    #endregion

    #region [ Companies ]

    private void SeedCompanies()
    {
        if (!_context.Set<Company>().Any())
        {
            var parners = new[]
            {
                new CompanyPartner(new Guid("1a0592e2-71f0-48cc-8267-0f8d75fe0a5e"), 54, new DateOnly(2022, 1, 1))
            };

            var phones = new[]
            {
                new CompanyPhone(new Phone("11","999999999")),
                new CompanyPhone(new Phone("11","988888888")),
                new CompanyPhone(new Phone("11","977777777")),
            };

            var company1 = new Company(
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
                ),
                partners: parners,
                phones: phones
            );

            _context.Set<Company>().AddRange(company1);

            _context.SaveChanges();
        }
    }

    #endregion
}
