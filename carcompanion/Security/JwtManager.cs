using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using carcompanion.Models;
using carcompanion.Results;
using carcompanion.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace carcompanion.Security
{
    public interface IJwtManager
    {
        Task<AuthenticationResult> AuthenticateUserAsync(User user);
        Task<AuthenticationResult> RefreshTokenAsync(Guid refreshTokenId, string accessToken);
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
                AccessToken = GenerateAccessToken(newAccessTokenJti, user.UserId, user.Email, user.RoleId),
                RefreshToken = await GenerateRefreshToken(newAccessTokenJti, user.UserId)
            };
                
            return authResult;
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(Guid refreshTokenId, string accessToken)
        {
            var validatedAccessToken = GetValidatedAccessToken(accessToken);
            
            if(validatedAccessToken == null)
                return new AuthenticationResult { Success = false, ErrorMessage = "Access token is not valid" };

            if(validatedAccessToken.ValidTo > DateTime.UtcNow)
                return new AuthenticationResult { Success = false, ErrorMessage = "Access token hadn't expired yet" };
            
            var accessTokenJti = Guid.Parse(validatedAccessToken.Id);
            var result = await _refreshtokenService.MakeRefreshTokenUsedAsync(refreshTokenId, accessTokenJti);

            if(!result.Success)
                return result;
                
            var newAccessTokenJti = Guid.NewGuid();
            GetFromClaims(validatedAccessToken.Claims, out var email, out var userId, out var role);

            result.AccessToken = GenerateAccessToken(newAccessTokenJti, userId, email, role);
            result.RefreshToken = await GenerateRefreshToken(newAccessTokenJti, userId);
            
            return result;
        }

        private void GetFromClaims(IEnumerable<Claim> tokenClaims, out string email, out Guid userId, out string userRole)
        {
            email = tokenClaims.First(x => x.Type.Equals("email")).Value;
            userId = Guid.Parse(tokenClaims.First(x => x.Type.Equals("sub")).Value);
            userRole = tokenClaims.First(x => x.Type.Equals("role")).Value;
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

        private string GenerateAccessToken(Guid accessTokenJti, Guid userId, string userEmail, string userRole)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);   
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, accessTokenJti.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, userEmail),
                    new Claim("role", userRole)
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