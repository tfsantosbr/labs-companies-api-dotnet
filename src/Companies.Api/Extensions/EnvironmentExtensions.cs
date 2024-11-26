using Companies.Infrastructure.Contexts;

namespace Companies.Api.Extensions;

public static class EnvironmentExtensions
{
    public static WebApplication MapEnvironmentConfigurations(this WebApplication app, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            UseDevelopmentSettings(app);
        }
        else if(environment.IsProduction())
        {
            UseProductionSettings(app);
        }
            
        return app;
    }

    public static WebApplication UseDevelopmentSettings(WebApplication app)
    {
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

    public static WebApplication UseProductionSettings(WebApplication app)
    {
        app.UseExceptionHandler();

        return app;
    }
}
