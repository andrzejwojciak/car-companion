using System;
using System.Threading.Tasks;
using AutoMapper;
using carcompanion.Contract.Security.Requests;
using carcompanion.Models;
using carcompanion.Repositories.Interfaces;
using carcompanion.Results;
using carcompanion.Security;
using carcompanion.Services.Interfaces;

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

        public async Task<AuthenticationResult> RegisterUserAsync(string email, string password)
        {
            if (await UserExistsByEmailAsync(email))
                return AuthFailed("User already exists");

            var user = new User {Email = email, Password = password, RoleId = "user"};

            if (!await AddUserAsync(user))
                return AuthFailed("Something went wrong");

            return await _jwtManager.AuthenticateUserAsync(user);
        }

        public async Task<AuthenticationResult> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null)
                return AuthFailed("Login or password is wrong.");

            if (!_hasher.VerifyHashedUserPassword(user.Password, password))
                return AuthFailed("Login or password is wrong.");

            return await _jwtManager.AuthenticateUserAsync(user);
        }

        public async Task<AuthenticationResult> AuthWithFacebookAsync(string accessToken)
        {
            var fbAuthResult = await _facebookAuthService.AuthUserByFbTokenAsync(accessToken);

            if (!fbAuthResult.Success)
                return AuthFailed(fbAuthResult.ErrorMessage);

            var user = await _userRepository.GetUserByEmailAsync(fbAuthResult.Email);

            if (user != null)
                return await _jwtManager.AuthenticateUserAsync(user);

            var newUser = new User {Email = fbAuthResult.Email, EmailConfirmed = true, RoleId = "user"};

            if (!await AddUserAsync(newUser))
                return AuthFailed("Something went wrong");

            return await _jwtManager.AuthenticateUserAsync(newUser);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(Guid refreshToken, string accessToken)
        {
            return await _jwtManager.RefreshTokenAsync(refreshToken, accessToken);
        }

        public async Task<LogoutResult> LogoutUserAsync(Guid refreshToken, Guid accessTokenJti)
        {
            var result = await _refreshTokenService.RemoveRefreshTokenAsync(refreshToken, accessTokenJti);
            
            if (!result.Success)
                return new LogoutResult {Success = result.Success};
            
            return new LogoutResult {Success = result.Success, ErrorMessage = result.ErrorMessage};
        }

        private static AuthenticationResult AuthFailed(string errorMessage)
        {
            return new AuthenticationResult {Success = false, ErrorMessage = errorMessage};
        }

        private async Task<bool> AddUserAsync(User user)
        {
            user.Password = _hasher.HashPassword(user.Password);
            return await _userRepository.AddUserAsync(user);
        }

        private async Task<bool> UserExistsByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user != null;
        }
    }
}