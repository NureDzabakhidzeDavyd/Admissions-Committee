using System.Data;
using System.Data.SqlClient;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Options;
using AdmissionsCommittee.Data.Helpers;
using Dapper;
using SqlKata;
using SqlKata.Compilers;

namespace AdmissionsCommittee.Data.Migrations;

public class MigrationDatabase
{
    private readonly DapperContext _dapperContext;

    public MigrationDatabase(DapperContext sqlConfiguration, DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public void CreateDatabase(string dbName)
    {
        var sqlCommand = new Query("sys.databases").Where("name", "=", dbName);
        var compiler = new SqlServerCompiler();
        SqlResult sqlResult = compiler.Compile(sqlCommand);
        var result = sqlResult.ToString();
        using var connection = _dapperContext.CreateConnection();
        var records = connection.Query(result);
        if (!records.Any())
            connection.Execute($"CREATE DATABASE {dbName}");
    }
}