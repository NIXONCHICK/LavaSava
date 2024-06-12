using LavaSava.Core.Models;

namespace LavaSava.Core.Interfaces.Auth;

public interface IJwtProvider
{
  string GenerateToken(User user);
  string DecodeToken(string? token);
}