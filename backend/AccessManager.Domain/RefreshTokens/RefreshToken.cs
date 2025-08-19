using AccessManager.Domain.Users;

namespace AccessManager.Domain.RefreshTokens;

public class RefreshToken
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid UserId { get; private set; } = default!;

    public string TokenHash { get; private set; } = default!;

    public DateTimeOffset ExpiresAtUtc { get; private set; } = default!;

    public bool IsRevoked { get; private set; } = false;

    public DateTimeOffset CreatedAtUtc { get; private set; } = DateTimeOffset.UtcNow;

    public string DeviceName { get; private set; } = default!;

    public User User { get; private set; } = default!;

    private RefreshToken() { }

    public RefreshToken(Guid userId, string tokenHash, DateTimeOffset expiresAtUtc, string deviceName)
    {
        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAtUtc = expiresAtUtc;
        DeviceName = deviceName;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public void Revoke()
    {
        IsRevoked = true;
    }

    public bool IsActive => IsRevoked == false && ExpiresAtUtc > DateTimeOffset.UtcNow;
}
