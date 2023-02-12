using System.Data;
using AdmissionsCommittee.Core.Options;
using Microsoft.Data.SqlClient;

namespace AdmissionsCommittee.Data;

public class DapperContext
{
    private readonly string _connectionString;
    public DapperContext(RepositoryConfiguration repositoryConfiguration)
    {
        _connectionString = repositoryConfiguration.DatabaseConnectionString;
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}