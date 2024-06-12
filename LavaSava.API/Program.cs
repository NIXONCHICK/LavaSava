using LavaSava.API.Endpoints;
using LavaSava.API.Extensions;
using LavaSava.Application.Services;
using LavaSava.Core.Interfaces.Auth;
using LavaSava.Core.Interfaces.Repositories;
using LavaSava.DataAccess.Postgres;
using LavaSava.DataAccess.Postgres.Repositories;
using LavaSava.Infrastructure;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

builder.Services.AddControllers();
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LavaSavaDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(LavaSavaDbContext)));
    });


services.AddApiAuthentication(configuration);
services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IUsersRepository, UsersRepository>();
services.AddScoped<IUsersService, UsersService>();


var app = builder.Build();

app.MapUsersEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
  MinimumSameSitePolicy = SameSiteMode.Strict,
  HttpOnly = HttpOnlyPolicy.Always,
  Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
