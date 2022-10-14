using Companies.Domain.Features.CompanyEmployeePositions;
using Companies.Domain.Features.CompanyMainActivities;
using Companies.Domain.Features.CompanyPartnerQualifications;

using Microsoft.EntityFrameworkCore;

namespace Companies.Infrastructure.Contexts;

public class CompaniesDatabaseSeed
{
    private readonly CompaniesContext _context;

    public CompaniesDatabaseSeed(CompaniesContext context)
    {
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        await Task.WhenAll(
            SeedCompanyEmployeePositionsAsync(),
            SeedCompanyMainActivitiesAsync(),
            SeedCompanyPartnerQualificationsAsync()
        );
    }

    #region [ Company Employee Positions ]

    private async Task SeedCompanyEmployeePositionsAsync()
    {
        if (!await _context.CompanyEmployeePositions.AnyAsync())
        {
            var companyEmployeePosition1 = new CompanyEmployeePosition(1, "Director");
            var companyEmployeePosition2 = new CompanyEmployeePosition(2, "Administrator");
            var companyEmployeePosition3 = new CompanyEmployeePosition(3, "Developer");
            var companyEmployeePosition4 = new CompanyEmployeePosition(4, "Architect");
            var companyEmployeePosition5 = new CompanyEmployeePosition(5, "Designer");

            await _context.CompanyEmployeePositions.AddRangeAsync(
                companyEmployeePosition1,
                companyEmployeePosition2,
                companyEmployeePosition3,
                companyEmployeePosition4,
                companyEmployeePosition5
                );

            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region [ Company Main Activities ]

    private async Task SeedCompanyMainActivitiesAsync()
    {
        if (!await _context.CompanyMainActivities.AnyAsync())
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


            await _context.CompanyMainActivities.AddRangeAsync(
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

            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region [ Company Partner Qualifications ]

    private async Task SeedCompanyPartnerQualificationsAsync()
    {
        if (!await _context.CompanyPartnerQualifications.AnyAsync())
        {
            var companyPartnerQualification1 = new CompanyPartnerQualification(5, "Administrador");
            var companyPartnerQualification2 = new CompanyPartnerQualification(10, "Diretor");
            var companyPartnerQualification3 = new CompanyPartnerQualification(16, "Presidente");
            var companyPartnerQualification4 = new CompanyPartnerQualification(22, "Sócio");
            var companyPartnerQualification5 = new CompanyPartnerQualification(54, "Fundador");

            await _context.CompanyPartnerQualifications.AddRangeAsync(
                companyPartnerQualification1,
                companyPartnerQualification2,
                companyPartnerQualification3,
                companyPartnerQualification4,
                companyPartnerQualification5
                );

            await _context.SaveChangesAsync();
        }
    }

    #endregion
}
