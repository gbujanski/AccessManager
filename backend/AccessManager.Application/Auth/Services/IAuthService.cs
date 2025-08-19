using AccessManager.Application.Auth.Commands;
using AccessManager.Application.Auth.Dtos;

namespace AccessManager.Application.Auth.Services;

public interface IAuthService
{
    Task<AuthTokensDto> LoginAsync(LoginCommand command);
    Task LogoutAsync(string token);
    Task<AuthTokensDto> RefreshAsync(string refreshToken);
}
