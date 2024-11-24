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
            await _httpClient.PostAsJsonAsync("/Users", users);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"/Users/{id}");
        }

        public async Task<Users> GetUserAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Users>($"Users/{id}");
        }

        public async Task<IEnumerable<Users>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Users>>("/Users");
        }

        public async Task UpdateUserAsync(Guid id, Users users)
        {
            await _httpClient.PutAsJsonAsync($"/Users/{id}", users);
        }
    }
}