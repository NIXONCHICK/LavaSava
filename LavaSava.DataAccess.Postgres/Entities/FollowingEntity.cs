namespace LavaSava.DataAccess.Postgres.Entities;

public class FollowingEntity
{
  public Guid Id { get; set; }
  public Guid FollowingId { get; set; } 
  public Guid UserId { get; set; }
  public UserEntity? User { get; set; }
}