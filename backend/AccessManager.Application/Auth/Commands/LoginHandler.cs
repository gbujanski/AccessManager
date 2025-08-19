using AccessManager.Application.Auth.Dtos;
using AccessManager.Application.Auth.Services;
using AccessManager.Domain.Users;

namespace AccessManager.Application.Auth.Commands;

public class LoginHandler
{
    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;

    public LoginHandler(IUserRepository repository,
                        IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    public async Task<AuthTokensDto> Handle(LoginCommand command)
    {
        return await _authService.LoginAsync(command);
    }
}
