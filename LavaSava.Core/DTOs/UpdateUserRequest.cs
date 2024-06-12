using System.ComponentModel.DataAnnotations;

namespace LavaSava.Core.DTOs;

public record UpdateUserRequest(
  string? Email,
  string? Password,
  string? UserName,
  string? Bio,
  string? Image);