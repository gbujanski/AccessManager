using AccessManager.Application.Auth.Dtos;
using AccessManager.Domain.Users;

namespace AccessManager.Application.Auth.Services;

public interface ITokenService
{
    AccessTokenDto GenerateAccessToken(User user);
    RefreshTokenDto GenerateRefreshToken();
    Task<bool> ValidateToken(string token);
    string HashToken(string token);
}