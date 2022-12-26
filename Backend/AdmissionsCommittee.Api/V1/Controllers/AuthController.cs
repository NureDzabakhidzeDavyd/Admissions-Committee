using AdmissionsCommittee.Contracts.V1.Response;
using AdmissionsCommittee.Core.Data;
using AdmissionsCommittee.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionsCommittee.Api.V1.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private const string redirectUrl = "https://localhost:4200/";
        private readonly IGoogleService _googleService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IGoogleService googleService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _googleService = googleService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Get refresh and access token by code
        /// </summary>
        /// <param name="authCode">The authorization code returned from the initial request</param>
        /// <returns>The object with refresh and access token + remaining token lifetime</returns>
        [HttpPost("code")]
        public async Task<IActionResult> RefreshAndAccessToken(string authCode)
        {
            var token = await _googleService.GetAccessTokenAsync(authCode);

            var result = _mapper.Map<GoogleAuthCodeResponse>(token);
            return Ok(result);
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<ActionResult> UserByAccessToken(string accessToken)
        {
            var user = await _googleService.GetUserProfile(accessToken);

            var result = _mapper.Map<UserProfileResponse>(user);

            return Ok(result);
        }

        /// <summary>
        /// Refresh access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessToken(string refreshToken)
        {
            var newAccessToken = await _googleService.RefreshAccessToken(refreshToken);

            var result = _mapper.Map<GoogleAuthCodeResponse>(newAccessToken);

            return Ok(result);
        }

        [HttpGet]

        /// <summary>
        /// Authentication by google auth
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("google")]
        public async Task<IActionResult> GoogleAuth(string token)
        {
            var googleTokenBody = await _googleService.GetAccessTokenAsync(token);

            var userInfo = await _googleService.GetUserProfile(googleTokenBody.AccessToken);

            var user = await _unitOfWork.UserRepository.GetUserByEmail(userInfo.Email);

            if (user is null)
            {
                userInfo.RefreshToken = googleTokenBody.RefreshToken;
                await _unitOfWork.UserRepository.CreateAsync(userInfo);
            }

            var result = _mapper.Map<UserProfileResponse>(userInfo);

            var jwtAuthenticationToken = _googleService.WriteJwtToken();
            Response.Headers.Add("Bearer", $"Bearer {jwtAuthenticationToken}");

            return Ok(result);
        }
    }
}
