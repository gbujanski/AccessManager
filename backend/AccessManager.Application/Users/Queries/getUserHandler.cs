using AccessManager.Domain.Users;
using AccessManager.Application.Users.Dtos;

namespace AccessManager.Application.Users.Queries;

public class GetUserHandler
{
    private readonly IUserRepository _repository;

    public GetUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserDto?> Handle(GetUserQuery query)
    {
        var user = await _repository.GetByIdAsync(query.Id);
        if (user == null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role
        };
    }
}
