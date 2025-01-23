using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Infrastructure.Services
{
    public interface IJwtTokenValidator
    {
        int ValidateToken(string token);
    }
}
