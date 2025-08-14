using AccessManager.Domain.Users;

namespace AccessManager.Application.Users.Commands;

public class CreateUserHandler
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateUserCommand command)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
        var user = new User(command.Id, command.Email, passwordHash, command.Role);

        await _repository.AddAsync(user);
    }
}
