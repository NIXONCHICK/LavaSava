namespace LavaSava.Core.Models;

public class User(
  Guid id, 
  string passwordHash, 
  string userName, 
  string email, 
  string? bio = null, 
  string? image = null,
  List<Guid>? followings = null)
{
  public Guid Id { get; private set; } = id;
  public string UserName { get; private set; } = userName;
  public string Email { get; private set; } = email;
  public string PasswordHash { get; private set; } = passwordHash;
  public string? Bio { get; private set; } = bio;
  public string? Image { get; private set; } = image;
  public ICollection<Guid>? Followings { get; private set; } = followings;
}