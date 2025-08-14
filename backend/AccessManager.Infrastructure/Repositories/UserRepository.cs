using AccessManager.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace AccessManager.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var entities = await _db.Users
            .OrderBy(u => u.Role)
            .ThenBy(u => u.Email)
            .ToListAsync();
        return entities;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var entity = await _db.Users.FindAsync(id);
        if (entity == null) return null;

        return entity;
    }

    public async Task DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        await _db.SaveChangesAsync();
    }
}