using RentingPlatform.Shared;

namespace RentingPlatform;

public interface IUsersService
{
    Task CreateUser(Users user);
    Task DeleteUser(Guid id);
    Task<Users> GetUser(Guid id);
    Task<List<Users>> GetAllUsers();
    Task UpdateUser(Users user);
}