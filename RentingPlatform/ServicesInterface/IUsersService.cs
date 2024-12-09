using RentingPlatform.Shared;

namespace RentingPlatform
{
    public interface IUsersService
    {
        Task CreateUser(Users users);
        Task<Users> GetUser(Guid id);
        Task<Users> GetUserByEmail(string email); // Add this
        Task<List<Users>> GetAllUsers();
        Task DeleteUser(Guid id);
        Task UpdateUser(Users newUser);
    }
}
