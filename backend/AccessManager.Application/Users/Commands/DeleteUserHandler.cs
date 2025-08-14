using AccessManager.Domain.Users;
using BCrypt.Net;

namespace AccessManager.Application.Users.Commands;

public class DeleteUserHandler
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteUserCommand command)
    {
        var user = await _repository.GetByIdAsync(command.Id);
        if (user == null)
            throw new KeyNotFoundException($"User with ID {command.Id} not found.");

        await _repository.DeleteAsync(user);
    }
}
