using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blog.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        
        public TokenService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GenerateToken(int accountId, string role)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        
    }
}