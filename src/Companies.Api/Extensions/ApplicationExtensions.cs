using Companies.Application.Abstractions.Models;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Commands.AddPartnerInCompany;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Commands.ImportCompanies;
using Companies.Application.Features.Companies.Commands.RemoveCompany;
using Companies.Application.Features.Companies.Commands.RemovePartnerFromCompany;
using Companies.Application.Features.Companies.Commands.UpdateCompany;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Features.Partners.Repositories;
using Companies.Infrastructure.Repositories;

namespace Companies.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Companies

        services.AddTransient<IHandler<CreateCompanyCommand, Response<Company>>, CreateCompanyCommandHandler>();
        services.AddTransient<IHandler<UpdateCompanyCommand, Response>, UpdateCompanyCommandHandler>();
        services.AddTransient<IHandler<RemoveCompanyCommand, Response>, RemoveCompanyCommandHandler>();
        services.AddTransient<IHandler<AddPartnerInCompany, Response<CompanyPartner>>, AddPartnerInCompanyHandler>();
        services.AddTransient<IHandler<RemovePartnerFromCompanyCommand, Response>, RemovePartnerFromCompanyCommandHandler>();
        services.AddTransient<IHandler<ImportCompaniesCommand, Response>, ImportCompaniesCommandHandler>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();

        // Partners

        services.AddTransient<IPartnerRepository, PartnerRepository>();

        return services;
    }
}
