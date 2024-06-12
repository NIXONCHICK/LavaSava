namespace LavaSava.Core.DTOs;

public record RegisterUserResponse(
  string Email,
  string Token,
  string UserName,
  string Bio,
  string Image);