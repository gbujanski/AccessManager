using System.ComponentModel.DataAnnotations;

namespace AccessManager.Application.Auth.Dtos;

public class AuthTokensDto
{
    [Required]
    public AccessTokenDto AccessToken { get; set; } = default!;

    [Required]
    public RefreshTokenDto RefreshToken { get; set; } = default!;

}
