using System.Net.Http.Json;
using RentingPlatform.Shared;

namespace RentingPlatform.UI.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        
        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> LoginUserAsync(Users loginUsers)
        {
            var user = await _httpClient.GetFromJsonAsync<Users>($"Users/{loginUsers.UserId}");
            if (user == null)
            {
                return "User not found.";
            }

            return user.Password == loginUsers.Password ? "Login successful." : "Invalid credentials.";
        }
    }
}