using System.ComponentModel.DataAnnotations;

namespace AccessManager.Application.Users.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    [Required]
    public required string Email { get; set; }
    
    [Required]
    public required string Role { get; set; }
}
