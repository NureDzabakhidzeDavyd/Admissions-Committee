namespace AdmissionsCommittee.Api.Installers;

public interface IInstaller
{
   public void InstallService(IServiceCollection services, IConfiguration configuration);
}