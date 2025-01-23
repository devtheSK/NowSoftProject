using NowSoft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
