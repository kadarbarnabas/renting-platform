using RentingPlatform.Shared;
using Microsoft.EntityFrameworkCore;

namespace RentingPlatform;

public class UsersService : IUsersService
{
    private readonly RentingPlatformContext _context;
    private readonly ILogger<UsersService> _logger;

    public UsersService(ILogger<UsersService> logger, RentingPlatformContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task CreateUser(Users users)
    {
        await _context.User.AddAsync(users);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User has been added");
    }

    public async Task<Users> GetUser(Guid id)
    {
        var user = await _context.User.FindAsync(id);
        _logger.LogInformation("User has been retrieved: {@User}", user);
        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await GetUser(id);
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User has been removed");
    }

    public async Task<List<Users>> GetAllUsers()
    {
        _logger.LogInformation("Every user has been fetched");
        return await _context.User.ToListAsync();
    }

    public async Task UpdateUser(Users newUser)
    {
        var user = await GetUser(newUser.UserId);
        user.Name = newUser.Name;
        user.Email = newUser.Email;
        user.Password = newUser.Password;
        _logger.LogInformation("{Users} has been updated", user);
    }
}