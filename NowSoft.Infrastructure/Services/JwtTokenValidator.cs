using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Services
{
    public class JwtTokenValidator : IJwtTokenValidator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // Parse the userId from the token
            var userIdClaim = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            else
            {
                throw new SecurityTokenException("Invalid token: User ID is not valid.");
            }
        }
    }
}
