using Companies.Application.Abstractions.Handlers;
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

        services.AddTransient<IHandler<CreateCompany, Response<Company>>, CreateCompanyHandler>();
        services.AddTransient<IHandler<UpdateCompany, Response>, UpdateCompanyHandler>();
        services.AddTransient<IHandler<RemoveCompany, Response>, RemoveCompanyHandler>();
        services.AddTransient<IHandler<AddPartnerInCompany, Response<CompanyPartner>>, AddPartnerInCompanyHandler>();
        services.AddTransient<IHandler<RemovePartnerFromCompany, Response>, RemovePartnerFromCompanyHandler>();
        services.AddTransient<IHandler<ImportCompanies, Response>, ImportCompaniesHandler>();
        services.AddTransient<ICompanyRepository, CompanyRepository>();

        // Partners

        services.AddTransient<IPartnerRepository, PartnerRepository>();

        return services;
    }
}
