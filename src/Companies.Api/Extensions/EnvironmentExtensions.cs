using Companies.Infrastructure.Contexts;

namespace Companies.Api.Extensions;

public static class EnvironmentExtensions
{
    public static WebApplication UseDevelopmentSettings(this WebApplication app, IHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
            return app;

        // swagger

        app.UseApiVersionedSwagger();

        // migrations

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CompaniesContext>();

        // seed sample data

        var databaseSeed = new CompaniesDatabaseSeed(context);
        databaseSeed.SeedData();

        return app;
    }

    public static WebApplication UseProductionSettings(this WebApplication app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
            return app;

        app.UseExceptionHandler();

        return app;
    }
}
