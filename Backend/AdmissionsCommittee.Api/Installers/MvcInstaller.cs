using AdmissionsCommittee.Api.Installers;

namespace TheatersOfTheCity.Api.Installers;

public class MvcInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpClient();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddCors(options =>
        {
            options.AddPolicy("angularApp", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("*");
            });
        });
        services.AddEndpointsApiExplorer();
    }
}