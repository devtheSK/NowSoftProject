using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NowSoft.Domain.Entities;
using NowSoft.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Commands
{
    public class SignupCommandHandler : IRequestHandler<SignupCommand>
    {
        private readonly AppDbContext _dbContext;

        public SignupCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var passwordHasher = new PasswordHasher<User>();
            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = passwordHasher.HashPassword(null, request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Device = request.Device,
                IPAddress = request.IPAddress
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(cancellationToken);


        }

    }
}
