using System.Net.Http.Json;
using RentingPlatform.Shared;

namespace RentingPlatform.UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateUserAsync(Users users)
        {
            var response = await _httpClient.PostAsJsonAsync("Users", users);
            if (!response.IsSuccessStatusCode)
            {
                // Handle the error or throw a custom exception
                throw new Exception($"Failed to create user: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"Users/{id}");
            if (!response.IsSuccessStatusCode)
            {
                // Handle the error or throw a custom exception
                throw new Exception($"Failed to delete user: {response.ReasonPhrase}");
            }
        }

        public async Task<Users> GetUserAsync(Guid id)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"Users/{id}");
            if (user == null)
            {
                throw new Exception($"User with ID {id} not found");
            }
            return user;
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Users>>("Users")
                   ?? Enumerable.Empty<Users>();
        }

        public async Task UpdateUserAsync(Guid id, Users users)
        {
            var response = await _httpClient.PutAsJsonAsync($"Users/{id}", users);
            if (!response.IsSuccessStatusCode)
            {
                // Handle the error or throw a custom exception
                throw new Exception($"Failed to update user: {response.ReasonPhrase}");
            }
        }
    }
}
