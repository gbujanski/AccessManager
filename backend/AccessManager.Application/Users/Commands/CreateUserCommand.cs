namespace AccessManager.Application.Users.Commands;

public class CreateUserCommand
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string Role { get; init; } = "user";
}
