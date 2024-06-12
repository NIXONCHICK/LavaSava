using System.ComponentModel.DataAnnotations;

namespace LavaSava.Core.DTOs;

public record LoginUserRequest(
  [Required] string Email, 
  [Required] string Password);