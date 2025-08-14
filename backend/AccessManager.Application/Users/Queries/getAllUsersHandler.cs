using AccessManager.Domain.Users;
using AccessManager.Application.Users.Dtos;

namespace AccessManager.Application.Users.Queries;

public class GetAllUsersHandler
{
    private readonly IUserRepository _repository;

    public GetAllUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> Handle()
    {
        var users = await _repository.GetAllAsync();
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        });
    }
}
