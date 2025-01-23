using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NowSoft.Domain.Entities;
using NowSoft.Infrastructure.Data;
using NowSoft.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Commands
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticateCommandHandler(AppDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticateResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);
            if (user == null) throw new UnauthorizedAccessException("Invalid credentials");

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result != PasswordVerificationResult.Success) throw new UnauthorizedAccessException("Invalid credentials");

            if (user.Balance == 0)
            {
                user.Balance = 5;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticateResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }
    }

}
