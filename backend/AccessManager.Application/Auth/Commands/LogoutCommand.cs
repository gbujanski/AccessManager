namespace AccessManager.Application.Auth.Commands;

public class LogoutCommand
{
    public string RefreshToken { get; init; } = default!;
}
