using AdmissionsCommittee.Business.Options;
using AdmissionsCommittee.Core.Domain;
using AdmissionsCommittee.Core.External;
using AdmissionsCommittee.Core.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;

namespace AdmissionsCommittee.Business.External
{
    public class GoogleService : IGoogleService
    {
        private const string UserInfoUrl = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";
        private const string AuthEndpoint = "https://oauth2.googleapis.com/token";

        private readonly ClientCredentials _clientCredentials;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<GoogleService> _logger;
        private readonly JwtSettings _jwtSettings;

        public GoogleService(ClientCredentials clientCredentials,
            IHttpClientFactory clientFactory,
            ILogger<GoogleService> logger,
            JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _clientCredentials = clientCredentials;
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<GoogleTokenBody> GetAccessTokenAsync(string code)
        {
            var queryParams = new Dictionary<string, string>
        {
            { "client_id", _clientCredentials.ClientId },
            { "client_secret", _clientCredentials.ClientSecret },
            { "grant_type", "authorization_code" },
            { "code", code},
            { "access_type", "offline" },
            {"redirect_uri", "https://developers.google.com/oauthplayground"},
        };

            var content = new FormUrlEncodedContent(queryParams);

            _logger.LogInformation("Google: Trying to request token");
            var authResponse = await _clientFactory.CreateClient().PostAsync(AuthEndpoint, content);
            authResponse.EnsureSuccessStatusCode();
            _logger.LogInformation("Google: refresh token was successfully received");

            var stringData = await authResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GoogleTokenBody>(stringData);

            return result ?? throw new ArgumentNullException($"can't deserialize access token");
        }

        public async Task<UserProfile> GetUserProfile(string accessToken)
        {
            var formattedUrl = string
                .Format(UserInfoUrl, accessToken);


            _logger.LogInformation("Auth: Trying to get user info");
            var authResponse = await _clientFactory.CreateClient().GetAsync(formattedUrl);
            authResponse.EnsureSuccessStatusCode();
            _logger.LogInformation("Auth: user profile was successfully received");

            var stringData = await authResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserProfile>(stringData);

            // specify exception
            return user ?? throw new NullReferenceException();
        }

        public async Task<GoogleTokenBody> RefreshAccessToken(string refreshToken)
        {
            var queryParams = new Dictionary<string, string>
        {
            { "client_id", _clientCredentials.ClientId },
            { "client_secret", _clientCredentials.ClientSecret },
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken },
        };

            var content = new FormUrlEncodedContent(queryParams);
            // using for CreateClient
            var authResponse = await _clientFactory.CreateClient().PostAsync(AuthEndpoint, content);
            authResponse.EnsureSuccessStatusCode();

            var stringData = await authResponse.Content.ReadAsStringAsync();
            var newAccessToken = JsonConvert.DeserializeObject<GoogleTokenBody>(stringData);

            return newAccessToken ?? throw new ArgumentNullException(newAccessToken?.Error);
        }

        public string WriteJwtToken()
        {
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _clientCredentials.ClientId,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.Unicode.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.Add(_jwtSettings.TokenLifetime));

            var jwtResult = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return jwtResult;
        }
    }
}
