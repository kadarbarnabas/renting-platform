using RentingPlatform.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        // Optional: Check for duplicate email before adding
        var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == users.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Attempted to create a user with duplicate email: {Email}", users.Email);
            throw new InvalidOperationException("A user with the same email already exists.");
        }

        await _context.User.AddAsync(users);
        await _context.SaveChangesAsync();


        _logger.LogInformation("User created and verification email sent: {Email}", users.Email);
    }

    public async Task<Users> GetUser(Guid id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", id);
        }
        else
        {
            _logger.LogInformation("User has been retrieved: {UserId}", user.UserId);
        }

        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await GetUser(id);
        if (user == null)
        {
            _logger.LogWarning("Attempted to delete a non-existent user with ID: {UserId}", id);
            throw new KeyNotFoundException("User not found.");
        }

        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User has been removed: {UserId}", id);
    }

    public async Task<List<Users>> GetAllUsers()
    {
        _logger.LogInformation("Fetching all users");
        return await _context.User.ToListAsync();
    }

    public async Task UpdateUser(Users newUser)
    {
        var user = await GetUser(newUser.UserId);
        if (user == null)
        {
            _logger.LogWarning("Attempted to update a non-existent user with ID: {UserId}", newUser.UserId);
            throw new KeyNotFoundException("User not found.");
        }

        // Update properties
        user.Name = newUser.Name;
        user.Email = newUser.Email;
        user.Password = newUser.Password;

        await _context.SaveChangesAsync();
        _logger.LogInformation("User has been updated: {UserId}", user.UserId);
    }

    public async Task<Users> GetUserByEmail(string email)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        if (user != null)
        {
            _logger.LogInformation("User has been retrieved by email: {Email}", email);
        }
        else
        {
            _logger.LogWarning("No user found with email: {Email}", email);
        }
        return user;
    }
}
