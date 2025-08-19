using System.ComponentModel.DataAnnotations;

namespace AccessManager.Application.Auth.Dtos;

public class AccessTokenDto
{
    [Required]
    public string Token { get; set; } = default!;

    [Required]
    public DateTime ExpiresAtUtc { get; set; } = default!;

}
