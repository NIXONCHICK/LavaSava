namespace LavaSava.Infrastructure;

public class JwtOptions
{
  public string SecretKey { get; set; } = string.Empty;
  public int ExpiryHours { get; set; }
}