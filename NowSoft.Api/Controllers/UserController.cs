using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NowSoft.Application.Commands;
using NowSoft.Application.Queries;
using NowSoft.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NowSoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand command)
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }

        [HttpPost("auth/balance")]
        public async Task<IActionResult> GetBalance(BalanceQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

    }
}
