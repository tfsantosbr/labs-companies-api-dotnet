using Companies.Domain.Base.ValueObjects;
using Companies.Domain.Features.CompanyMainActivities;
using Companies.Domain.Features.CompanyPartnerQualifications;
using Companies.Domain.Features.Partners;

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
    }

    #region [ Company Main Activities ]

    private void SeedCompanyMainActivities()
    {
        if (!_context.CompanyMainActivities.Any())
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


            _context.CompanyMainActivities.AddRange(
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
        if (!_context.CompanyPartnerQualifications.Any())
        {
            var companyPartnerQualification1 = new CompanyPartnerQualification(5, "Administrador");
            var companyPartnerQualification2 = new CompanyPartnerQualification(10, "Diretor");
            var companyPartnerQualification3 = new CompanyPartnerQualification(16, "Presidente");
            var companyPartnerQualification4 = new CompanyPartnerQualification(22, "Sócio");
            var companyPartnerQualification5 = new CompanyPartnerQualification(54, "Fundador");

            _context.CompanyPartnerQualifications.AddRange(
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
        if (!_context.Partners.Any())
        {
            var partner1 = new Partner(new CompleteName("Tiago", "Santos"), new Email("tiago@email.com"));
            var partner2 = new Partner(new CompleteName("Iran", "Nunes"), new Email("iran@email.com"));
            var partner3 = new Partner(new CompleteName("Bruna", "Oliveira"), new Email("bruna@email.com"));
            var partner4 = new Partner(new CompleteName("Maria", "Gorete"), new Email("maria@email.com"));
            var partner5 = new Partner(new CompleteName("Natalia", "Lourenço"), new Email("natalia@email.com"));
            var partner6 = new Partner(new CompleteName("Roberto", "Justus"), new Email("roberto@email.com"));
            var partner7 = new Partner(new CompleteName("Kim", "Katagiri"), new Email("kim@email.com"));
            var partner8 = new Partner(new CompleteName("Renan", "Oliveira"), new Email("renan@email.com"));
            var partner9 = new Partner(new CompleteName("Will", "Toshio"), new Email("will@email.com"));
            var partner10 = new Partner(new CompleteName("Carol", "Castro"), new Email("carol@email.com"));

            _context.Partners.AddRange(
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
}
