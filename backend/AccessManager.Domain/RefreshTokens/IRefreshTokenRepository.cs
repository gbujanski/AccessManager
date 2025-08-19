namespace AccessManager.Domain.RefreshTokens;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByIdAsync(Guid id);
    Task<RefreshToken?> GetByUserIdAsync(Guid userId);
    Task<RefreshToken?> GetByTokenAsync(string tokenHash);
    Task DeleteAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken refreshToken);
}
