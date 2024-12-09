using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using RentingPlatform.Shared;

namespace RentingPlatform.Controllers
{
    [ApiController]
    [Route("[controller]/google")]
    public class LoginController : Controller
    {
        private readonly IUsersService _userService;

        public LoginController(IUsersService usersService)
        {
            _userService = usersService;
        }


        [HttpGet("google-respone")]
        public async Task<ActionResult> GotResponse()
        {
            return Ok();
        }

        [HttpGet("/signin-google")]
        public async Task<IActionResult> SignInGoogleAsync(string code)
        {
            var tokenRequestUrl = "https://oauth2.googleapis.com/token";
            var client = new HttpClient();

            var requestContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },  // The authorization code from the query string
                { "client_id", "783585114234-otbkc9057k4om2o6o13tarc975eto6cq.apps.googleusercontent.com" },  // Your Google Client ID
                { "client_secret", "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" },  // Your Google Client Secret
                { "redirect_uri", "http://localhost:8080/signin-google" },  // The same redirect URI used in the frontend
                { "grant_type", "authorization_code" }  // The OAuth grant type
            });

            // Send the request to Google's token endpoint
            var response = await client.PostAsync(tokenRequestUrl, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            // Deserialize the JSON to extract the id_token
            var jsonDocument = JsonDocument.Parse(responseContent);
            var idToken = jsonDocument.RootElement.GetProperty("id_token").GetString();

            if (idToken == null)
            {
                return BadRequest("id_token not found in response.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                return BadRequest("Invalid id_token.");
            }
            // Extract claims (profile data)
            var name = jsonToken?.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var email = jsonToken?.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
            var picture = jsonToken?.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

       
            var user = _userService.GetUserByEmail(email);
            if (user.Result == null)
            {
                var newUser = new Users {
                    UserId = new Guid(),
                    Name = name,
                    Email = email,
                    Password = string.Empty,
                    Picture = picture
                };
                await _userService.CreateUser(newUser);
                return Redirect($"http://localhost:5222/{newUser.UserId}");
            }
            else if (user == null || user.Result == null)
            {
                // Handle the error, maybe redirect to an error page or log the issue
                return BadRequest();  // Example: Redirect to an error page
            }
            else
            {
                return Redirect($"http://localhost:5222/{user.Result.UserId}");
            }
        }
    }
}
        
