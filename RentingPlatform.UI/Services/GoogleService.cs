using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RentingPlatform.Shared;


namespace RentingPlatform.UI.Services
{
    public class GoogleService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public GoogleService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task LoginWithGoogleDirectly()
        {
            // Construct the Google OAuth URL with your client ID and redirect URI
            var googleAuthUrl = "https://accounts.google.com/o/oauth2/v2/auth?client_id=783585114234-otbkc9057k4om2o6o13tarc975eto6cq.apps.googleusercontent.com&scope=openid%20profile%20email&response_type=code&redirect_uri=http://localhost:8080/signin-google";
            
            // Redirect the user to Google's OAuth URL
            _navigationManager.NavigateTo(googleAuthUrl, true);  // `true` performs a full page redirect
        }
    }
}