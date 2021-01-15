using System.Threading.Tasks;
using carcompanion.Contract.Security.Requests;
using carcompanion.Contract.Security.Responses;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.Security.ApiRoutes;
using carcompanion.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using carcompanion.Results;
using carcompanion.Extensions;

namespace carcompanion.Controllers.Security
{
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost(Auth.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterUserAsync(request.Email, request.Password);
            return GenerateResponse(result);
        }

        [AllowAnonymous]
        [HttpPost(Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginUserAsync(request.Email, request.Password);
            return GenerateResponse(result);
        }

        [AllowAnonymous]
        [HttpPost(Auth.AuthWithFacebook)]
        public async Task<IActionResult> AuthWithFacebook([FromBody] AuthWithFacebookRequest request)
        {
            var result = await _authService.AuthWithFacebookAsync(request.AccessToken);
            return GenerateResponse(result);
        }

        [AllowAnonymous]
        [HttpPost(Auth.Refresh)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken, request.AccessToken);
            return GenerateResponse(result);
        }

        [HttpPost(Auth.Logout)]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
        {
            var result = await _authService.LogoutUserAsync(request.RefreshToken, HttpContext.GetAccessTokenJti());
            return result.Success ? StatusCode(200, new {Success = true}) : StatusCode(404, result);
        }

        private ActionResult GenerateResponse(AuthenticationResult result) =>
            result.Success
                ? StatusCode(200,
                    new AuthSuccessResponse
                        {Success = true, RefreshToken = result.RefreshToken, AccessToken = result.AccessToken})
                : StatusCode(400, new AuthFailedResponse {Success = false, Error = result.ErrorMessage});
    }
}