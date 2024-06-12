using System.ComponentModel.DataAnnotations;

namespace LavaSava.Core.DTOs;

public record DeleteUserRequest(
  [Required] Guid Id);