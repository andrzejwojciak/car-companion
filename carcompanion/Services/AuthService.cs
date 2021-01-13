using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.Security.Requests;
using carcompanion.Contract.V1.Responses.Interfaces;
using carcompanion.Data;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using carcompanion.Results;
using carcompanion.Security;
using carcompanion.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace carcompanion.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _hasher;
        private readonly IMapper _mapper;
        private readonly IJwtManager _jwtManager;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshtokenService _refreshTokenService;
        private readonly IFacebookAuthService _facebookAuthService;

        public AuthService(IPasswordHasher hasher,
                           IMapper mapper,
                           IJwtManager jwtManager,
                           IUserRepository userRepository,
                           IRefreshtokenService refreshTokenService,
                           IFacebookAuthService facebookAuthService)
        {
            _facebookAuthService = facebookAuthService;
            _hasher = hasher;
            _mapper = mapper;
            _jwtManager = jwtManager;
            _userRepository = userRepository;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request)
        {
            if (await UserExistsByEmailAsync(request.Email))
                return AuthFailed("User already exists");
            
            var user = _mapper.Map<User>(request);
            
            if (!await AddUserAsync(user))
                return AuthFailed("Something went wrong");
            
            return await _jwtManager.AuthenticateUserAsync(user);
        }
        
        public async Task<AuthenticationResult> LoginUserAsync(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            
            if(user == null)
                return AuthFailed("Login or password is wrong.");
            
            if(!_hasher.VerifyHashedUserPassword(user.Password, request.Password))
                return AuthFailed("Login or password is wrong.");
            
            return await _jwtManager.AuthenticateUserAsync(user);
        }

        public async Task<AuthenticationResult> AuthWithFacebookAsync(AuthWithFacebookRequest request)
        {            
            var fbAuthResult = await _facebookAuthService.AuthUserByFbTokenAsync(request.AccessToken);        

            if (!fbAuthResult.Success)
                return AuthFailed(fbAuthResult.ErrorMessage);

            var user = await _userRepository.GetUserByEmailAsync(fbAuthResult.Email);

            if (user != null)
                return await _jwtManager.AuthenticateUserAsync(user);

            var newUser = new User { Email = fbAuthResult.Email, EmailConfirmed = true };    

            if (!await AddUserAsync(newUser))
                return AuthFailed("Somehting went wrong");                

            return await _jwtManager.AuthenticateUserAsync(newUser);
        }
        
        public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            return await _jwtManager.RefreshTokenAsync(request.RefreshToken, request.AccessToken);
        }

        public async Task<LogoutResult> LogoutUserAsync(Guid refreshToken, Guid accessTokenJti)
        {
            var result = await _refreshTokenService.RemoveRefreshTokenAsync(refreshToken, accessTokenJti);    
            return new LogoutResult{ Success = result.Success, ErrorMessage = result.ErrorMessage};
        }

        private AuthenticationResult AuthFailed(string errorMessage)
        {
            return new AuthenticationResult { Success = false, ErrorMessage = errorMessage };
        }

        private async Task<bool> AddUserAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await _userRepository.AddUserAsync(user);
        }
        
        private async Task<bool> UserExistsByEmailAsync(string email) 
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user == null ? false : true;
        }
    }
}