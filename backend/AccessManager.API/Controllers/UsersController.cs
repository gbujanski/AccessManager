using AccessManager.Application.Users.Commands;
using AccessManager.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AccessManager.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly CreateUserHandler _createUserHandler;
    private readonly GetUserHandler _getUserHandler;
    private readonly GetAllUsersHandler _getAllUsersHandler;
    private readonly DeleteUserHandler _deleteUserHandler;
    private readonly EditUserHandler _editUserHandler;

    public UsersController(
        CreateUserHandler createUserHandler,
        GetUserHandler getUserHandler,
        GetAllUsersHandler getAllUsersHandler,
        DeleteUserHandler deleteUserHandler,
        EditUserHandler editUserHandler
    )
    {
        _createUserHandler = createUserHandler;
        _getUserHandler = getUserHandler;
        _getAllUsersHandler = getAllUsersHandler;
        _deleteUserHandler = deleteUserHandler;
        _editUserHandler = editUserHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        await _createUserHandler.Handle(command);

        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _getAllUsersHandler.Handle();
        return Ok(users);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await _getUserHandler.Handle(new GetUserQuery { Id = id });

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditUser([FromRoute] Guid id, [FromBody] EditUserCommand command)
    {
        await _editUserHandler.Handle(id, command);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        await _deleteUserHandler.Handle(new DeleteUserCommand { Id = id });

        return NoContent();
    }
}
