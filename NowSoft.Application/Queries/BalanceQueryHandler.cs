using MediatR;
using Microsoft.EntityFrameworkCore;
using NowSoft.Infrastructure.Data;
using NowSoft.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Queries
{
    public class BalanceQueryHandler : IRequestHandler<BalanceQuery, BalanceResponse>
    {
        private readonly AppDbContext _context;
        private readonly IJwtTokenValidator _jwtTokenValidator;

        public BalanceQueryHandler(AppDbContext context, IJwtTokenValidator jwtTokenValidator)
        {
            _context = context;
            _jwtTokenValidator = jwtTokenValidator;
        }

        public async Task<BalanceResponse> Handle(BalanceQuery request, CancellationToken cancellationToken)
        {
            var userId = _jwtTokenValidator.ValidateToken(request.Token);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
            {
                throw new Exception("Invalid token");
            }

            return new BalanceResponse
            {
                Balance = user.Balance
            };
        }
    }
}
