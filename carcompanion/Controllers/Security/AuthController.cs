using System.Threading.Tasks;
using carcompanion.Contract.Security.Requests;
using carcompanion.Contract.Security.Responses;
using carcompanion.Models;
using carcompanion.Security;
using Microsoft.AspNetCore.Mvc;
using static carcompanion.Contract.Security.ApiRoutes;
using AutoMapper;
using carcompanion.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using carcompanion.Results;

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
            var result = await _authService.RegisterUserAsync(request);
            return GenerateResponse(result); 
        }
        
        [AllowAnonymous]
        [HttpPost(Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginUserAsync(request);
            return GenerateResponse(result);             
        }
        
        [AllowAnonymous]
        [HttpPost(Auth.Refresh)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request);
            return GenerateResponse(result);
        }     

        [HttpPost(Auth.Logout)]
        public IActionResult Logout()
        {
            //TODO: Validate request and destroy refresh token

            return Ok("Work in progress...");
        }

        private ActionResult GenerateResponse(AuthenticationResult result)
        {
            if(result.Success)
                return StatusCode(200, new AuthSuccessResponse{ Success = true, RefreshToken = result.RefreshToken, AccessToken = result.AccessToken });
            
            return StatusCode(400, new AuthFailedResponse{ Success = false, Error = result.ErrorMessage });
        }  
    }
}