using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;

    public UsersController(IUsersService usersService)
    {
        _userService = usersService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] Users users)
    {
        var existingUser = await _userService.GetUser(users.UserId);

        if (existingUser is not null)
        {
            return Conflict();
        }

        await _userService.CreateUser(users);

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Users>> DeletetUser(Guid id)
    {
        var user = await _userService.GetUser(id);

        if (user is null)
        {
            return NotFound();
        }

        await _userService.DeleteUser(id);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Users>> GetUser(Guid id)
    {
        var user = await _userService.GetUser(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<List<Users>>> GetAllUsers()
    {
        return Ok(await _userService.GetAllUsers());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] Users newUser)
    {
        if (id != newUser.UserId)
        {
            return BadRequest();
        }

        var existingUser = await _userService.GetUser(id);

        if (existingUser is null)
        {
            return NotFound();
        }

        await _userService.UpdateUser(newUser);

        return Ok();
    }
}