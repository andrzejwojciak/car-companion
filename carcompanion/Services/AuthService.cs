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

        public AuthService(IPasswordHasher hasher, IMapper mapper, IJwtManager jwtManager, IUserRepository userRepository)
        {
            _hasher = hasher;
            _mapper = mapper;
            _jwtManager = jwtManager;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> RegisterUserAsync(RegisterRequest request)
        {
            if(await UserExistsByEmailAsync(request.Email))
                return AuthFailed("User already exists");
            
            var user = _mapper.Map<User>(request);
            
            if(!await AddUserAsync(user))
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
        
        public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            return await _jwtManager.RefreshTokenAsync(request.AccessToken, request.RefreshToken);
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