using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using AccessManager.Application.Auth.Configuration;
using AccessManager.Domain.Users;
using System.Security.Cryptography;
using AccessManager.Application.Auth.Dtos;

namespace AccessManager.Application.Auth.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<JwtOptions> _options;

    public TokenService(IOptions<JwtOptions> options)
    {
        _options = options;
    }

    public AccessTokenDto GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_options.Value.AccessTokenExpirationMinutes),
            SigningCredentials = creds,
            Issuer = _options.Value.Issuer,
            Audience = _options.Value.Audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AccessTokenDto
        {
            Token = tokenHandler.WriteToken(token),
            ExpiresAtUtc = tokenDescriptor.Expires.Value
        };
    }

    public RefreshTokenDto GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        var refreshToken = Convert.ToBase64String(bytes);
        var expiresAtUtc = DateTimeOffset.UtcNow.AddDays(_options.Value.RefreshTokenExpirationDays);

        return new RefreshTokenDto
        {
            Token = refreshToken,
            ExpiresAtUtc = expiresAtUtc
        };
    }

    public string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public async Task<bool> ValidateToken(string token)
    {
        return await Task.FromResult(true);
    }
}
