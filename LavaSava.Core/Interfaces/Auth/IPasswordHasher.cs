namespace LavaSava.Core.Interfaces.Auth;

public interface IPasswordHasher
{
  string GenerateHash(string password);
  bool VerifyHash(string password, string hash);
}