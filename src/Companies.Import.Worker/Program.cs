using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Models;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Features.Companies;
using Companies.Application.Features.Companies.Commands;
using Companies.Application.Features.Companies.Handlers;
using Companies.Application.Features.Companies.Repositories;
using Companies.Import.Worker.Consumers;
using Companies.Infrastructure.Contexts;
using Companies.Infrastructure.Contexts.Persistence;
using Companies.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        // application

        services.AddHostedService<ImportCompanyConsumer>();

        services.AddScoped<IHandler<CreateCompany, Response<Company>>, CreateCompanyHandler>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        // contexts

        services.AddDbContext<CompaniesContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")!));

        // add unit of work

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    })
    .Build();

host.Run();
