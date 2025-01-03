using Companies.Application.Abstractions.Handlers;
using Companies.Application.Abstractions.Persistence;
using Companies.Application.Abstractions.Validations;
using Companies.Application.Features.Companies.Commands.CreateCompany;
using Companies.Application.Features.Companies.Events;
using Companies.Application.Features.Companies.Models;
using Companies.Application.Features.Companies.Repositories;
using Companies.Import.Worker.Consumers;
using Companies.Infrastructure.Contexts;
using Companies.Infrastructure.Contexts.Persistence;
using Companies.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        // application

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(ICommandValidator<>), typeof(CommandValidator<>));
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CompanyCreatedDomainEvent>());

        services.AddHostedService<ImportCompanyConsumer>();
        services.AddScoped<ICommandHandler<CreateCompanyCommand, CompanyDetails>, CreateCompanyCommandHandler>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddValidatorsFromAssemblyContaining<CreateCompanyCommandValidator>();

        // contexts

        services.AddDbContext<CompaniesContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Postgres")!));

        // add unit of work

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    })
    .Build();

host.Run();
