using LavaSava.Core.Models;

namespace LavaSava.Core.Interfaces.Repositories;

public interface IUsersRepository
{
  Task AddUserAsync(User user);
  Task<User> GetByEmailAsync(string email);
  Task<User> GetByIdAsync(Guid id);
  Task<User> UpdateUserAsync(Guid id, UpdatedUser updatedUser);
  Task<User?> GetUserWithFollowingsAsync(string userName);
  Task DeleteUserByIdAsync(Guid id);
  Task<User?> FollowUserAsync(Guid followerId, string followingName);
  Task<User?> UnfollowUserAsync(Guid followerId, string followingName);
}