using MediatR;
using Microsoft.AspNetCore.Identity;
using NowSoft.Domain.Entities;
using NowSoft.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NowSoft.Application.Commands
{
    public record SignupCommand(
    string UserName,
    string Password,
    string FirstName,
    string LastName,
    string Device,
    string IPAddress
) : IRequest;

}
