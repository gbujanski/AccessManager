using AccessManager.Application.Auth.Dtos;
using AccessManager.Application.Auth.Services;

namespace AccessManager.Application.Auth.Commands;

public class RefreshHandler
{
    private readonly IAuthService _authService;

    public RefreshHandler(IAuthService authService)
    {
        _authService = authService;
    }
    
    public async Task<AuthTokensDto> Handle(RefreshCommand command)
    {
        return await _authService.RefreshAsync(command.RefreshToken);
    }
}