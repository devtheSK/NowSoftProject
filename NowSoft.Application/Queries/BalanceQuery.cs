using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Queries
{
    public class BalanceQuery : IRequest<BalanceResponse>
    {
        public string Token { get; set; }
    }
}
