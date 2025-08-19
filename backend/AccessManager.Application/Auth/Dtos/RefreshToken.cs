using System.ComponentModel.DataAnnotations;

namespace AccessManager.Application.Auth.Dtos;

public class RefreshTokenDto
{
    [Required]
    public string Token { get; set; } = default!;
    
    [Required]
    public DateTimeOffset ExpiresAtUtc { get; set; } = default!;
}
