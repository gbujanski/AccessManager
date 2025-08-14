namespace AccessManager.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task DeleteAsync(User user);
    Task UpdateAsync();
}
