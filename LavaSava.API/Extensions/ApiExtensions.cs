using System.Text;
using LavaSava.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LavaSava.API.Extensions;

public static class ApiExtensions
{

  public static void AddApiAuthentication(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
    
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.TokenValidationParameters = new()
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            context.Token = context.Request.Cookies["Bearer"];
            
            return Task.CompletedTask;
          }
        };
      });
    services.AddAuthorization();
  }
}
