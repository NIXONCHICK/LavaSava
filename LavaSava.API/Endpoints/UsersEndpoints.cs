using LavaSava.Core.DTOs;
using LavaSava.Core.Interfaces.Auth;

namespace LavaSava.API.Endpoints;

public static class UsersEndpoints
{
  public static void MapUsersEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapPost("register", Register);
    app.MapPost("login", Login);
    app.MapPost("delete", Delete)
      .RequireAuthorization();
  }

  private static async Task<IResult> Register(RegisterUserRequest request, IUsersService usersService)
  {
    await usersService.Register(request.UserName, request.Email, request.Password);
    return Results.Ok();
  }
  
  private static async Task<IResult> Login(
    LoginUserRequest request, 
    IUsersService usersService,
    HttpContext context)
  {
    var token = await usersService.Login(request.Email, request.Password);
    
    return Results.Ok(token);
  }
  
  private static async Task<IResult> Delete(DeleteUserRequest request, IUsersService usersService)
  {
    return Results.Ok();
  }
}