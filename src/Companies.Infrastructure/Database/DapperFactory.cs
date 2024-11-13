using System.Data;
using Companies.Application.Abstractions.Database;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Companies.Infrastructure.Database;

public class DapperFactory(IConfiguration configuration) : IDapperFactory
{
    public IDbConnection CreateConnection() => new NpgsqlConnection(configuration.GetConnectionString("Postgres"));
}
