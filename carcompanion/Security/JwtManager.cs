using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using carcompanion.Models;
using carcompanion.Services.Interfaces;
using carcompanion.Services.Results;
using Microsoft.IdentityModel.Tokens;

namespace carcompanion.Security
{
    public interface IJwtManager
    {
        Task<AuthenticationResult> AuthenticateUserAsync(User user);
        Task<AuthenticationResult> RefreshTokenAsync(string accessToken, string refreshToken);
    }

    public class JwtManager : IJwtManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshtokenService _refreshtokenService;

        public JwtManager(JwtSettings jwtSettings, IRefreshtokenService refreshtokenService)
        {
            _jwtSettings = jwtSettings;
            _refreshtokenService = refreshtokenService;
        }

        public async Task<AuthenticationResult> AuthenticateUserAsync(User user)
        {               
            var newAccessTokenJti = Guid.NewGuid();      

            var authResult = new AuthenticationResult
            {
                Success = true,
                AccessToken = GenerateAccessToken(newAccessTokenJti, user.UserId, user.Email),
                RefreshToken = await GenerateRefreshToken(newAccessTokenJti, user.UserId)
            };
                
            return authResult;
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string accessToken, string refreshToken)
        {
            var validatedAccessToken = GetValidatedAccessToken(accessToken);

            if(validatedAccessToken == null)
                return new AuthenticationResult { Success = false, ErrorMessage = "Token is not vaild" };

            if(validatedAccessToken.ValidTo > DateTime.UtcNow)
                return new AuthenticationResult { Success = false, ErrorMessage = "Access token hadn't expired yet" };
            
            var accessTokenJti = validatedAccessToken.Id;
            var refreshTokenResult = await _refreshtokenService.ValidateRefreshTokenAsync(accessTokenJti, refreshToken);
            var authenticationResult = new AuthenticationResult{ Success = refreshTokenResult.Success, ErrorMessage = refreshTokenResult.ErrorMessage };

            if(!authenticationResult.Success)
                return authenticationResult;
                
            var tokenClaims = validatedAccessToken.Claims;
            var newAccessTokenJti = Guid.NewGuid();

            var email = tokenClaims.First(x => x.Type.Equals("email")).Value;
            var userId = Guid.Parse(tokenClaims.First(x => x.Type.Equals("sub")).Value);

            authenticationResult.AccessToken = GenerateAccessToken(newAccessTokenJti, userId, email);
            authenticationResult.RefreshToken = await GenerateRefreshToken(newAccessTokenJti, userId);
            
            return authenticationResult;
        }

        private JwtSecurityToken GetValidatedAccessToken(string accessToken)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();

            if(!securityTokenHandler.CanReadToken(accessToken))
                return null;
            
            var tokenValidationParameters = new TokenValidationParameters
                      {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SigningKey)),
                            ValidateIssuer = true,                      
                            ValidIssuer = _jwtSettings.Issuer,
                            ValidateAudience = false,      
                            ValidateLifetime = false,
                            RequireExpirationTime = false,
                            ClockSkew = TimeSpan.Zero    
                      };

            try
            {             
                securityTokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var token);   
                return (JwtSecurityToken)token;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateAccessToken(Guid accessTokenJti, Guid userId, string userEmail)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);   
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, accessTokenJti.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userEmail)
                }),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_jwtSettings.AccessTokenLifeTime)),
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);    
            
            return tokenHandler.WriteToken(token);
        }

        private async Task<string> GenerateRefreshToken(Guid accessTokenJti, Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                AccessTokenJti = accessTokenJti,
                UserId = userId,
                ExpirationDate = DateTime.UtcNow.AddDays(double.Parse(_jwtSettings.RefreshTokenLifeTime))
            };

            await _refreshtokenService.SaveRefreshTokenAsync(refreshToken);

            return refreshToken.RefreshTokenId.ToString();
        }
    }
}