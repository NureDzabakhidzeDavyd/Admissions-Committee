using AdmissionsCommittee.Data.Migrations;
using FluentMigrator.Runner;

namespace AdmissionsCommittee.Api.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var databaseService = scope.ServiceProvider.GetRequiredService<MigrationDatabase>();
        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        databaseService.CreateDatabase("AdmissionsCommittee");
        migrationService.ListMigrations();
        migrationService.MigrateUp();
        return host;
    }
}