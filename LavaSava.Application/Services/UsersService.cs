using LavaSava.Core.DTOs;
using LavaSava.Core.Interfaces.Auth;
using LavaSava.Core.Interfaces.Repositories;
using LavaSava.Core.Models;

namespace LavaSava.Application.Services;

public class UsersService(
  IUsersRepository usersRepository, 
  IPasswordHasher passwordHasher,
  IJwtProvider jwtProvider) : IUsersService
{
  public async Task<RegisterUserResponse> Register(string userName, string email, string password)
  {
    var passwordHash = passwordHasher.GenerateHash(password);
    var user = new User(
      Guid.NewGuid(), 
      passwordHash,
      userName, 
      email);
    await usersRepository.AddUserAsync(user);
    var token = jwtProvider.GenerateToken(user);
    var userResponse = new RegisterUserResponse(email, token, userName, "", "");
    return userResponse;
  }
  
  public async Task<RegisterUserResponse> Login(string email, string password)
  {
    var user = await usersRepository.GetByEmailAsync(email);
    if (user == null)
    {
      throw new Exception("User not found");
    }
    
    if (!passwordHasher.VerifyHash(password, user.PasswordHash))
    {
      throw new Exception("Invalid password");
    }
    
    var token = jwtProvider.GenerateToken(user);
    var userResponse = new RegisterUserResponse(
      email,
      token,
      user.UserName,
      user.Bio ?? "",
      user.Image ?? "");
    
    return userResponse;
  }

  public async Task<RegisterUserResponse> GetCurrentUser(string? token)
  {
    
    var userId = jwtProvider.DecodeToken(token);
    var user = await usersRepository.GetByIdAsync(Guid.Parse(userId));
    
    var userResponse = new RegisterUserResponse(
      user.Email,
      token ?? "",
      user.UserName,
      user.Bio ?? "",
      user.Image ?? "");

    return userResponse;
  }
  
  public async Task<RegisterUserResponse> UpdateUser(UpdatedUser updatedUser, string? token)
  {
    if(updatedUser.PasswordHash != null)
    {
      updatedUser.PasswordHash = passwordHasher.GenerateHash(updatedUser.PasswordHash);
    }
    var userId = jwtProvider.DecodeToken(token);
    var user = await usersRepository.UpdateUserAsync(Guid.Parse(userId), updatedUser);
    var userResponse = new RegisterUserResponse(
      user.Email,
      token ?? "",
      user.UserName,
      user.Bio ?? "",
      user.Image ?? "");

    return userResponse;
  }
  
  public async Task<ProfileResponse> GetProfile(string userName)
  {
    var user = await usersRepository.GetUserWithFollowingsAsync(userName);
    if(user == null)
    {
      throw new Exception("User not found");
    }

    var followings = user.Followings;
    var profileResponse = new ProfileResponse(
      user.UserName,
      user.Bio ?? "",
      user.Image ?? "",
      followings ?? []);
    
    return profileResponse;
  }
  
  public async Task<ProfileResponse> FollowUser(string? token, string userName)
  {
    var userId = jwtProvider.DecodeToken(token);
    var user = await usersRepository.FollowUserAsync(Guid.Parse(userId), userName);
    if(user == null)
    {
      throw new Exception("User not found");
    }

    var followings = user.Followings;
    var profileResponse = new ProfileResponse(
      user.UserName,
      user.Bio ?? "",
      user.Image ?? "",
      followings ?? []);
    
    return profileResponse;
  }
  
  public async Task<ProfileResponse> UnfollowUser(string? token, string userName)
  {
    var userId = jwtProvider.DecodeToken(token);
    var user = await usersRepository.UnfollowUserAsync(Guid.Parse(userId), userName);
    if(user == null)
    {
      throw new Exception("User not found");
    }

    var followings = user.Followings;
    var profileResponse = new ProfileResponse(
      user.UserName,
      user.Bio ?? "",
      user.Image ?? "",
      followings ?? []);
    
    return profileResponse;
  }
}