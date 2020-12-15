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

namespace carcompanion.Controllers.Security
{
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJwtManager _jwtManager;

        public AuthController(IUserService userService, IMapper mapper, IJwtManager jwtManager)
        {
            _jwtManager = jwtManager;
            _mapper = mapper;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);

            if(user == null)
                return NotFound("User doesn't exist");
            
            if(!_userService.IsPasswordMatch(user, request.Password))
                return BadRequest("Login or password is wrong.");
            
            return await AuthenticateUser(user);
        }

        [AllowAnonymous]
        [HttpPost(Auth.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {            
            if(await _userService.UserExistsByEmailAsync(request.Email))
                return BadRequest("User already exists!");
            
            var newUser = _mapper.Map<User>(request);
            
            if(!await _userService.RegisterUserAsync(newUser))
                return BadRequest("Something went wrong!");
            
            return await AuthenticateUser(newUser);
        }

        [AllowAnonymous]
        [HttpPost(Auth.Refresh)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var refreshTokenResult = await _jwtManager.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

            if(!refreshTokenResult.Success)
                return Unauthorized(refreshTokenResult.ErrorMessage);

            return Ok(refreshTokenResult);
        }        

        [HttpPost(Auth.Logout)]
        public async Task<IActionResult> Logout()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return Ok("TODO");
        }

        private async Task<IActionResult> AuthenticateUser(User user)
        {
            var authenticationResult = await _jwtManager.AuthenticateUserAsync(user);            
            return CreateAuthenticationResponse(authenticationResult);
        }

        private IActionResult CreateAuthenticationResponse(AuthenticationResult result)
        {
            //TODO: Use automapper to map responses
            if(!result.Success)
                return Unauthorized( new AuthFailedResponse{ Success = false, Error = result.ErrorMessage } );

            return Ok( new AuthSuccessResponse{ Success = true, AccessToken = result.AccessToken, RefreshToken = result.RefreshToken } );
        }        

    }
}