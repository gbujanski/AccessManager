using AccessManager.Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace AccessManager.Infrastructure.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _db;

    public RefreshTokenRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(RefreshToken token)
    {
        _db.RefreshTokens.Add(token);
        await _db.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id)
    {
        return await _db.RefreshTokens.FindAsync(id);
    }

    public async Task<RefreshToken?> GetByUserIdAsync(Guid userId)
    {
        return await _db.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string tokenHash)
    {
        return await _db.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.TokenHash == tokenHash);
    }

    public async Task DeleteAsync(RefreshToken token)
    {
        _db.RefreshTokens.Remove(token);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(RefreshToken token)
    {
        _db.RefreshTokens.Update(token);
        await _db.SaveChangesAsync();
    }
}


