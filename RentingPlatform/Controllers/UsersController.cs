using RentingPlatform.Shared;
using Microsoft.AspNetCore.Mvc;

namespace RentingPlatform.Controllers
{
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
            // Check if email already exists
            var existingUser = await _userService.GetUserByEmail(users.Email);
            if (existingUser != null)
            {
                return Conflict("A user with the same email already exists.");
            }

            await _userService.CreateUser(users);
            return CreatedAtAction(nameof(GetUser), new { id = users.UserId }, users);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Users>> GetUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] Users newUser)
        {
            if (id != newUser.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _userService.GetUser(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            // Optionally, check for email conflicts
            var emailConflict = await _userService.GetUserByEmail(newUser.Email);
            if (emailConflict != null && emailConflict.UserId != id)
            {
                return Conflict("Email is already in use.");
            }

            await _userService.UpdateUser(newUser);
            return NoContent();
        }
    }
}
