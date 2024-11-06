using Microsoft.EntityFrameworkCore;
using Companies.Infrastructure.Contexts;
using Companies.Infrastructure.Contexts.Persistence;
using Companies.Infrastructure.Services.Messaging;
using Companies.Application.Abstractions.Messaging;
using Companies.Application.Abstractions.Persistence;

namespace Companies.Api.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        // contexts

        services.AddDbContext<CompaniesContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres"), builder =>
                {
                    builder.MigrationsAssembly("Companies.Infrastructure");
                }));

        // add unit of work

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // add message broker

        services.AddScoped<IMessageBroker, MessageBroker>();

        return services;
    }

    public static void MigrateAndSeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CompaniesContext>();

            context.Database.Migrate();

            var databaseSeed = new CompaniesDatabaseSeed(context);

            databaseSeed.SeedData();
        }
    }
}
