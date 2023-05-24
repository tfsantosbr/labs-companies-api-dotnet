# Companies API

## Documentations

- [Tasks](./documentation/../documentation/TASKS.md)
- [Use Cases](./documentation/../documentation/USECASES.md)
- [Technical Debts](./documentation/TECHNICAL_DEBTS.md)

## Patterns Used In This Application

- [Domain Driven Design](https://balta.io/cursos/modelando-dominios-ricos)
- [Folder By Feature Structure](https://github.com/tfsantosbr/dotnet-folder-by-feature-structure)
- [CQRS + Mediator](https://balta.io/blog/aspnet-core-cqrs-mediator)
- [Repository](https://learning.eximia.co/videos/repositorios/)

## Applying Development Certifications

Necessary for run application in HTTPS, locally and docker

- [ASP.NET With Docker and HTTPS](https://josiahmortenson.dev/blog/2020-06-08-aspnetcore-docker-https)

Run this commands to apply development certifications:

```bash
dotnet dev-certs https --clean
dotnet dev-certs https -ep $USERPROFILE/.aspnet/https/aspnetapp.pfx -p dev@123
dotnet dev-certs https --trust

```

## Running Docker Infrastructure Dependencies

You will need [Docker Desktop](https://docs.docker.com/desktop/install/windows-install/) to run this commands

```bash
docker-compose up -d
```

## Migrations

You will need [.NET EF Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) to run this commands

```bash
# add migration
dotnet ef migrations add <MIGRATION_NAME> -p src/Companies.Infrastructure/ -c CompaniesContext -s src/Companies.Api -o Contexts/Migrations

# remove migration
dotnet ef migrations remove -p src/Companies.Infrastructure/ -c CompaniesContext -s src/Companies.Api

# update database
dotnet ef database update -p src/Companies.Infrastructure -c CompaniesContext -s src/Companies.Api

# generate scripts for manual database update
dotnet ef migrations script -p src/Companies.Infrastructure/ -c CompaniesContext -s src/Companies.Api -o ./scripts/migrations.sql
```

## Running API Locally

You will need [.NET CLI](https://dotnet.microsoft.com/en-us/download) to run this commands

```bash
dotnet restore
dotnet build
dotnet run --project src/Companies.Api
```
