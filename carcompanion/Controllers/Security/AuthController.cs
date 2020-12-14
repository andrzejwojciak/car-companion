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

            var response = new AuthSuccessResponse
            {
                JwtToken = _jwtManager.GenerateToken(user)
            };

            return Ok(response);
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

            var response = new AuthSuccessResponse
            {
                JwtToken = _jwtManager.GenerateToken(newUser)
            };

            return Ok(response);
        }

        [HttpPost(Auth.Refresh)]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            return Ok("Refreshed!");
        }        

        [HttpPost(Auth.Logout)]
        public IActionResult Logout()
        {
            return Ok("Logged out!");
        }

    }
}