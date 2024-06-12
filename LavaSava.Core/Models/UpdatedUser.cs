using System.ComponentModel.DataAnnotations;

namespace LavaSava.Core.Models;

public class UpdatedUser(
  string? email = null,
  string? username = null,
  string? passwordHash = null,
  string? image = null,
  string? bio = null)
{
  public string? Email { get; set; } = email;
  public string? UserName { get; set; } = username;
  public string? PasswordHash { get; set; } = passwordHash;
  public string? Image { get; set; } = image;
  public string? Bio { get; set; } = bio;
}