using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LavaSava.Core.Interfaces.Auth;
using LavaSava.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LavaSava.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
  private readonly JwtOptions _options = options.Value;

  public string GenerateToken(User user)
  {
    Claim[] claims = [ new Claim("userId", user.Id.ToString()) ];
    
    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(_options.SecretKey)),
      SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(
      claims: claims,
      signingCredentials: signingCredentials,
      expires: DateTime.UtcNow.AddHours(_options.ExpiryHours));
    
    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
    
    return tokenValue;
  }
  
  public string DecodeToken(string? token)
  {
    var handler = new JwtSecurityTokenHandler();
    var jwtToken = handler.ReadJwtToken(token);
    var userIdClaim = jwtToken.Claims.First(claim => claim.Type == "userId");
    
    return userIdClaim.Value;
  }
}