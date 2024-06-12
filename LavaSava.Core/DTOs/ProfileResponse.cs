namespace LavaSava.Core.DTOs
{

  public record ProfileResponse(
    string UserName, 
    string? Bio,
    string? Image,
    ICollection<Guid>? Following
    );
}