using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NowSoft.Application.DTOs;
using NowSoft.Domain.Entities;
using NowSoft.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Commands
{
    //public class AuthenticateCommand : IRequest<UserDto>
    //{
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string IPAddress { get; set; }
    //    public string Device { get; set; }
    //    public string Browser { get; set; }
    // }

    public record AuthenticateCommand(string UserName, string Password, string IPAddress, string Device) : IRequest<AuthenticateResponse>;


    //public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    //{
    //    private readonly AppDbContext _dbContext;
    //    private readonly IConfiguration _configuration;

    //    public AuthenticateCommandHandler(AppDbContext dbContext, IConfiguration configuration)
    //    {
    //        _dbContext = dbContext;
    //        _configuration = configuration;
    //    }

    //    public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    //    {
    //        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);
    //        if (user == null) throw new UnauthorizedAccessException("Invalid credentials");

    //        var passwordHasher = new PasswordHasher<User>();
    //        var result = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);
    //        if (result != PasswordVerificationResult.Success) throw new UnauthorizedAccessException("Invalid credentials");

    //        if (user.Balance == 0)
    //        {
    //            user.Balance = 5; // First-time login bonus
    //            _dbContext.Users.Update(user);
    //            await _dbContext.SaveChangesAsync(cancellationToken);
    //        }

    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(new[]
    //            {
    //            new Claim(ClaimTypes.Name, user.UserName),
    //            new Claim("FirstName", user.FirstName),
    //            new Claim("LastName", user.LastName)
    //        }),
    //            Expires = DateTime.UtcNow.AddDays(7),
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };
    //        var token = tokenHandler.CreateToken(tokenDescriptor);

    //        return new AuthenticateResponse
    //        {
    //            FirstName = user.FirstName,
    //            LastName = user.LastName,
    //            Token = tokenHandler.WriteToken(token)
    //        };
    //    }
    //}

}
