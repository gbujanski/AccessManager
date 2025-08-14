namespace AccessManager.Domain.Users;

public class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = default!;

    public string PasswordHash { get; private set; } = default!;

    public string Role { get; private set; } = default!;

    private User() { }

    public User(Guid id, string email, string passwordHash, string role)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public void UpdateEmail(string email)
    {
        Email = email;
    }

    public void UpdateRole(string role)
    {
        Role = role;
    }
}
