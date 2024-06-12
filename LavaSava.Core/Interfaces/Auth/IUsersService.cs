using LavaSava.Core.DTOs;
using LavaSava.Core.Models;

namespace LavaSava.Core.Interfaces.Auth;

public interface IUsersService
{
  Task<RegisterUserResponse> Register(string userName, string email, string password);
  Task<RegisterUserResponse> Login(string email, string password);
  Task<RegisterUserResponse> GetCurrentUser(string? token);
  Task<RegisterUserResponse> UpdateUser(UpdatedUser updatedUser, string? token);
  Task<ProfileResponse> GetProfile(string userName);
  Task<ProfileResponse> FollowUser(string? token, string userName);
  Task<ProfileResponse> UnfollowUser(string? token, string userName);
}