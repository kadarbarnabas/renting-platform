using RentingPlatform.Shared;

namespace RentingPlatform.UI.Services;

public interface IUserService
{
    Task CreateUserAsync(Users users);

    Task DeleteUserAsync(Guid id);

    Task<Users> GetUserAsync(Guid id);

    Task<IEnumerable<Users>> GetAllUsersAsync();
    
    Task UpdateUserAsync(Guid id, Users users);
}