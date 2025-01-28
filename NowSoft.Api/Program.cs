using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NowSoft.Api.Middleware;
using NowSoft.Application.Commands;
using NowSoft.Application.Queries;
using NowSoft.Infrastructure.Data;
using NowSoft.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies([typeof(Program).Assembly,
                                        typeof(SignupCommand).Assembly,
                                        typeof(AuthenticateCommand).Assembly,
                                        typeof(BalanceQuery).Assembly]); 
                            });



builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
