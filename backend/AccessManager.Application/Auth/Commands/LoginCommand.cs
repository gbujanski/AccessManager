namespace AccessManager.Application.Auth.Commands;

public class LoginCommand
{
    public string Email { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string DeviceName { get; init; } = default!;
}
