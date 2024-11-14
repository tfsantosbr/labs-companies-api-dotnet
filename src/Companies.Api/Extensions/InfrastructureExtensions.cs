using Microsoft.EntityFrameworkCore;
using Companies.Infrastructure.Contexts;
using Companies.Infrastructure.Contexts.Persistence;
using Companies.Infrastructure.Services.Messaging;
using Companies.Application.Abstractions.Messaging;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Features.Companies.Repositories;
using Companies.Application.Features.Partners.Repositories;
using Companies.Infrastructure.Repositories;
using Companies.Application.Abstractions.Database;
using Companies.Infrastructure.Database;

namespace Companies.Api.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // contexts

        services.AddDbContext<ICompaniesContext, CompaniesContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres"), 
                builder => builder.MigrationsAssembly("Companies.Infrastructure")));

        // Dapper factory

        services.AddSingleton<IDapperFactory, DapperFactory>();

        // add unit of work

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // add message broker

        services.AddScoped<IMessageBroker, MessageBroker>();

        // Repositories

        services.AddTransient<ICompanyRepository, CompanyRepository>();
        services.AddTransient<IPartnerRepository, PartnerRepository>();

        return services;
    }
}
