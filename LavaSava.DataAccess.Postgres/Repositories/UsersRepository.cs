using LavaSava.Core.Interfaces.Repositories;
using LavaSava.Core.Models;
using LavaSava.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace LavaSava.DataAccess.Postgres.Repositories;

public class UsersRepository(LavaSavaDbContext dbContext) : IUsersRepository
{
  public async Task AddUserAsync(User user)
  {
    var userEntity = new UserEntity
    {
      Id = user.Id,
      UserName = user.UserName,
      Email = user.Email,
      PasswordHash = user.PasswordHash
    };
    await dbContext.Users.AddAsync(userEntity);
    await dbContext.SaveChangesAsync();
  }

  public async Task<User> GetByEmailAsync(string email)
  {
    var userEntity = await dbContext.Users
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Email == email);
    if (userEntity == null)
    {
      throw new Exception("User not found");
    }

    var user = new User(
      userEntity.Id, 
      userEntity.PasswordHash,
      userEntity.UserName, 
      userEntity.Email,
      userEntity.Bio,
      userEntity.Image);
    return user;
  }
  public async Task<User> GetByIdAsync(Guid id)
  {
    var userEntity = await dbContext.Users
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == id);
    if (userEntity == null)
    {
      throw new Exception("User not found");
    }
    var user = new User(
      userEntity.Id,
      userEntity.PasswordHash,
      userEntity.UserName, 
      userEntity.Email,
      userEntity.Bio,
      userEntity.Image);
    
    return user;
  }
  public async Task<User> UpdateUserAsync(Guid id, UpdatedUser updatedUser)
  {
    var userEntity = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    if(userEntity == null)
    {
      throw new Exception("User not found");
    }
    
    if (!string.IsNullOrEmpty(updatedUser.Email))
    {
      userEntity.Email = updatedUser.Email;
    }

    if (!string.IsNullOrEmpty(updatedUser.UserName))
    {
      userEntity.UserName = updatedUser.UserName;
    }

    if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
    {
      userEntity.PasswordHash = updatedUser.PasswordHash;
    }

    if (!string.IsNullOrEmpty(updatedUser.Image))
    {
      userEntity.Image = updatedUser.Image;
    }

    if (!string.IsNullOrEmpty(updatedUser.Bio))
    {
      userEntity.Bio = updatedUser.Bio;
    }

    dbContext.Users.Update(userEntity);
    await dbContext.SaveChangesAsync();
    var user = new User(
      userEntity.Id,
      userEntity.PasswordHash,
      userEntity.UserName,
      userEntity.Email,
      userEntity.Bio,
      userEntity.Image);

    return user;
  }
  
  public async Task<User?> GetUserWithFollowingsAsync(string userName)
{
  var userEntity = await dbContext.Users
    .AsNoTracking()
    .Include(x => x.Followings)
    .FirstOrDefaultAsync(u => u.UserName == userName);
  var user = userEntity == null ? null : new User(
    userEntity.Id,
    userEntity.PasswordHash,
    userEntity.UserName,
    userEntity.Email,
    userEntity.Bio,
    userEntity.Image,
    userEntity.Followings.Select(f => f.FollowingId).ToList());


  return user;
}
  
  public async Task<User?> FollowUserAsync(Guid followerId, string followingName)
  {
    var follower = await dbContext.Users
      .AsNoTracking()
      .Include(x => x.Followings)
      .FirstOrDefaultAsync(u => u.Id == followerId);
    
    var following = await dbContext.Users
      .FirstOrDefaultAsync(u => u.UserName == followingName);
    
    if(follower == null || following == null)
    {
        throw new Exception("Follower or following not found");
    }
    var followingEntity = new FollowingEntity
    {
        Id = Guid.NewGuid(),
        FollowingId = following.Id,
        UserId = followerId
    };
    
    await dbContext.Followings.AddAsync(followingEntity);
    await dbContext.SaveChangesAsync();
    follower.Followings.Add(followingEntity);
    
    var user = new User(
      follower.Id,
      follower.PasswordHash,
      follower.UserName,
      follower.Email,
      follower.Bio,
      follower.Image,
      follower.Followings.Select(f => f.FollowingId).ToList());


    return user;
  }
  
  public async Task<User?> UnfollowUserAsync(Guid followerId, string followingName)
  {
    var follower = await dbContext.Users
      .Include(x => x.Followings)
      .FirstOrDefaultAsync(u => u.Id == followerId);
    
    var following = await dbContext.Users
      .FirstOrDefaultAsync(u => u.UserName == followingName);
    
    if(follower == null || following == null)
    {
      throw new Exception("Follower or following not found");
    }
    var followingEntity = follower.Followings.FirstOrDefault(f => f.FollowingId == following.Id);
    if(followingEntity == null)
    {
      throw new Exception("Following not found");
    }
    dbContext.Followings.Remove(followingEntity);
    await dbContext.SaveChangesAsync();
    
    var user = new User(
      follower.Id,
      follower.PasswordHash,
      follower.UserName,
      follower.Email,
      follower.Bio,
      follower.Image,
      follower.Followings.Select(f => f.FollowingId).ToList());

    return user;
  }
  
  public async Task DeleteUserByIdAsync(Guid id)
  {
    var userEntity = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    if(userEntity == null)
    {
      throw new Exception("User not found");
    }
    dbContext.Users.Remove(userEntity);
    await dbContext.SaveChangesAsync();
  }
  
}