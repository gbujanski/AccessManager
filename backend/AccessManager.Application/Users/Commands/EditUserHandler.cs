using AccessManager.Domain.Users;

namespace AccessManager.Application.Users.Commands;

public class EditUserHandler
{
    private readonly IUserRepository _repository;

    public EditUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(Guid id, EditUserCommand command)
    {
        //get user from db
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }
        user.UpdateEmail(command.Email);
        user.UpdateRole(command.Role);
        await _repository.UpdateAsync();
    }
}
