using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NowSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // Cast the validated token to JwtSecurityToken
                var jwtToken = validatedToken as JwtSecurityToken;

                // Ensure the token is valid
                if (jwtToken == null)
                {
                    throw new SecurityTokenException("Invalid token.");
                }

                // Extract the NameIdentifier claim
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    throw new SecurityTokenException("User ID claim is missing.");
                }

                // Parse the UserId to an integer
                if (!int.TryParse(userIdClaim, out var userId))
                {
                    throw new SecurityTokenException("User ID claim is invalid.");
                }

                return userId; // Return the user ID
            }
            catch (Exception ex)
            {
                // Log the exception or handle as needed
                throw new SecurityTokenException("Token validation failed.", ex);
            }
        }
    }
}
