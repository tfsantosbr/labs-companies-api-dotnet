using System.Data;

namespace Companies.Application.Abstractions.Database;

public interface IDapperFactory
{
    public IDbConnection CreateConnection();
}
