namespace LavaSava.DataAccess.Postgres.Entities
{
  public class UserEntity
  {
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string? Image { get; set; }
    public List<FollowingEntity> Followings = [];
  }
}