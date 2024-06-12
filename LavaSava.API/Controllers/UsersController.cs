using LavaSava.Core.DTOs;
using LavaSava.Core.Interfaces.Auth;
using LavaSava.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LavaSava.API.Controllers;

[ApiController]
[Route("api")]
public class UsersController(
  IUsersService usersService) : ControllerBase
{
  [HttpPost("users/login")]
  public async Task<IActionResult> Login(LoginUserRequest request)
  {
    var user = await usersService
      .Login(request.Email, request.Password);
    Response.Cookies.Append("Bearer", user.Token);
    var userResponse = new { user };
    
    return Ok(userResponse);
  }
  
  [HttpPost("users")]
  public async Task<IActionResult> Register(RegisterUserRequest request)
  { 
    var user = await usersService
      .Register(request.UserName, request.Email, request.Password);
    Response.Cookies.Append("Bearer", user.Token);
    var userResponse = new { user };
    
    return Ok(userResponse);
  }

  [Authorize]
  [HttpGet("user")]
  public async Task<IActionResult> GetCurrentUser()
  {
    var token = Request.Cookies["Bearer"];
    var user = await usersService.GetCurrentUser(token);
    var userResponse = new { user };
    
    return Ok(userResponse);
  }

  [Authorize]
  [HttpPut("user")]
  public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
  {
    var token = Request.Cookies["Bearer"];
    var updatedUser = new UpdatedUser
    {
      Email = request.Email,
      UserName = request.UserName,
      PasswordHash = request.Password,
      Image = request.Image,
      Bio = request.Bio
    };
    
    var user = await usersService.UpdateUser(updatedUser, token);
    var userResponse = new { user };

    return Ok(userResponse);
  }
  
  [HttpGet("profiles/{userName}")]
  public async Task<IActionResult> GetProfile(string userName)
  {
    var profile = await usersService.GetProfile(userName);
    var profileResponse = new { profile };
    
    return Ok(profileResponse);
  }
  
  [Authorize]
  [HttpPost("profiles/{userName}/follow")]
  public async Task<IActionResult> FollowUser(string userName)
  {
    var token = Request.Cookies["Bearer"];
    var profile = await usersService.FollowUser(token, userName);
    var profileResponse = new { profile };
    
    return Ok(profileResponse);
  }
  
  [Authorize]
  [HttpDelete("profiles/{userName}/follow")]
  public async Task<IActionResult> UnfollowUser(string userName)
  {
    var token = Request.Cookies["Bearer"];
    var profile = await usersService.UnfollowUser(token, userName);
    var profileResponse = new { profile };
    
    return Ok(profileResponse);
  }
}