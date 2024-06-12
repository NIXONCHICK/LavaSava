using System.ComponentModel.DataAnnotations;

namespace LavaSava.Core.DTOs;

public record RegisterUserRequest(
  [Required] string UserName, 
  [Required] string Email, 
  [Required] string Password);