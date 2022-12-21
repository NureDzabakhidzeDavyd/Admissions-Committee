using AdmissionsCommittee.Api.Installers;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Options;
using AdmissionsCommittee.Core.Services;
using AdmissionsCommittee.Data;
using AdmissionsCommittee.Data.Helpers;
using AdmissionsCommittee.Data.Repository;

namespace TheatersOfTheCity.Api.Installers;

public class DbInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfiguration = configuration.GetSection(nameof(RepositoryConfiguration)).Get<RepositoryConfiguration>();
        services.AddSingleton(databaseConfiguration);

        services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IRankRepository, RankRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IWorkingRepository, WorkingRepository>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IStatementRepository, StatementRepository>();
        services.AddScoped<IEieRepository, EieRepository>();
        services.AddScoped<IStatisticRepository, StatisticRepository>();
        services.AddScoped<ICoefficientRepository, CoefficientRepository>();
        services.AddScoped<IMarkRepository, MarkRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISeeder, Seeder>();
        services.AddScoped<IQueryBuilder, QueryBuilder>();
    }
}