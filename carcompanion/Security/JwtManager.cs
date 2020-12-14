using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using carcompanion.Data;
using carcompanion.Models;
using Microsoft.IdentityModel.Tokens;

namespace carcompanion.Security
{
    public interface IJwtManager
    {
        AuthenticationResult AuthenticateUser(User user);        
    }

    public class JwtManager : IJwtManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _context;

        public JwtManager(JwtSettings jwtSettings, ApplicationDbContext context)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        public AuthenticationResult AuthenticateUser(User user)
        {               
            var newAccessTokenJti = Guid.NewGuid();      

            var newAccessToken = GenerateAccessToken(newAccessTokenJti, user.UserId, user.Email);
            var newRefreshToken = GenerateRefreshToken(newAccessTokenJti, user.UserId);

            var authResult = new AuthenticationResult
            {
                Success = true,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return authResult;
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

        private string GenerateRefreshToken(Guid accessTokenJti, Guid userId)
        {
            var refreshToken = new RefreshToken
            {
                AccessTokenJti = accessTokenJti,
                UserId = userId,
                ExpirationDate = DateTime.UtcNow.AddDays(double.Parse(_jwtSettings.RefreshTokenLifeTime))
            };

            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();

            return refreshToken.RefreshTokenId.ToString();
        }
    }
}