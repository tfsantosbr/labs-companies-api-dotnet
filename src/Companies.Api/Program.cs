using Companies.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorrelation();
builder.Services.AddHealthChecks(configuration);
builder.Services.AddSerilog(configuration);
builder.Services.AddBearerAuthentication(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructure(configuration);
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureJsonOptions();
builder.Services.ConfigureExceptionHandlers(environment);
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.UseHealthChecks();
app.MapVersionedEndpoints();

app.UseDevelopmentSettings(environment);
app.UseProductionSettings(environment);

app.UseCorrelationContext();
app.UseSerilogRequestLogging();
app.UseAuthentication();

app.Run();
