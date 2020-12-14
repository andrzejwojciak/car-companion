using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public JwtManager(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public AuthenticationResult AuthenticateUser(User user)
        {
            var newAccessToken = GenerateAccessToken(user);
            var newRefreshToken = GenerateRefreshToken(newAccessToken);

            var authResult = new AuthenticationResult
            {
                Success = true,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            return authResult;
        }

        private string GenerateAccessToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);            
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email)
                }),

                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_jwtSettings.AccessTokenLifeTime)),
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            

            var token = tokenHandler.CreateToken(tokenDescriptor);    

            return tokenHandler.WriteToken(token);

        }

        private string GenerateRefreshToken(string AccessToken)
        {
            return "";
        }
    }
}