using System.Net;
using AccessManager.Application.Auth.Commands;
using AccessManager.Application.Auth.Dtos;
using AccessManager.Domain.RefreshTokens;
using AccessManager.Domain.Users;

namespace AccessManager.Application.Auth.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthTokensDto> LoginAsync(LoginCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        await _refreshTokenRepository.AddAsync(new RefreshToken(
            user.Id,
            _tokenService.HashToken(refreshToken.Token),
            refreshToken.ExpiresAtUtc,
            command.DeviceName
        ));

        return new AuthTokensDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task LogoutAsync(string token)
    {
        var tokenHash = _tokenService.HashToken(token);
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(tokenHash);
        if (refreshToken == null)
        {
            throw new InvalidOperationException("Refresh token not found.");
        }

        await _refreshTokenRepository.DeleteAsync(refreshToken);
    }

    public async Task<AuthTokensDto> RefreshAsync(string token)
    {
        var decodedToken = WebUtility.UrlDecode(token);
        var tokenHash = _tokenService.HashToken(decodedToken);
        Console.WriteLine($"Decoded Token: {decodedToken}");
        Console.WriteLine($"Token Hash: {tokenHash}");

        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(tokenHash);
        if (refreshToken == null || !refreshToken.IsActive)
        {
            throw new InvalidOperationException("Refresh token not found or expired.");
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        refreshToken.Revoke();

        await _refreshTokenRepository.UpdateAsync(refreshToken);
        await _refreshTokenRepository.AddAsync(new RefreshToken(
            user.Id,
            _tokenService.HashToken(newRefreshToken.Token),
            newRefreshToken.ExpiresAtUtc,
            refreshToken.DeviceName
        ));

        return new AuthTokensDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
