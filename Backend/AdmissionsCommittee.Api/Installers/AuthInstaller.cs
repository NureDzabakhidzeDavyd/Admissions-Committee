using AdmissionsCommittee.Business.External;
using AdmissionsCommittee.Business.Options;
using AdmissionsCommittee.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AdmissionsCommittee.Api.Installers;

public class AuthInstaller : IInstaller
{
    public void InstallService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IGoogleService, GoogleService>();
        var clientCredentials = configuration.GetSection(nameof(ClientCredentials)).Get<ClientCredentials>();
        services.AddSingleton(clientCredentials);

        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = clientCredentials.ClientId,
                ValidateIssuer = true,
        
                ValidateAudience = false,
                RequireExpirationTime = true,
        
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(jwtSettings.Secret)),
                ValidateIssuerSigningKey = true
            };
        });
        services.AddAuthorization();
    }
}