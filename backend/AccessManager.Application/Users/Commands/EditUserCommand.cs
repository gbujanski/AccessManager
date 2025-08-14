namespace AccessManager.Application.Users.Commands;

public class EditUserCommand
{
    public string Email { get; init; } = default!;
    public string Role { get; init; } = default!;
}
