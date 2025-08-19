using AccessManager.Application.Auth.Services;

namespace AccessManager.Application.Auth.Commands;
public class LogoutHandler
{
    private readonly IAuthService _authService;

    public LogoutHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task Handle(LogoutCommand command)
    {
        await _authService.LogoutAsync(command.RefreshToken);
    }
}