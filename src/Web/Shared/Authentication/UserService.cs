using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Shared.Authentication
{
    public class UserService
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonSerializerOptions;
        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<User?> TryAuthorize()
        {
            var res = await httpClient.GetAsync("/api/account/Authorize");
            if (res.IsSuccessStatusCode)
            {
                var user = JsonSerializer.Deserialize<User>(await res.Content.ReadAsStringAsync(), jsonSerializerOptions);
                return user;
            }
            else
            {
                return default;
            }
        }
        public async Task<User?> Login(string username, string password)
        {
            var response = await httpClient.PostAsJsonAsync("/api/account/Login", new UserLogin(username, password));
            if (response.IsSuccessStatusCode)
            {
                var user = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions);
                return user;
            }
            else
                return default;

        }
        public async Task<User?> Register(UserRegister user)
        {
            var response = await httpClient.PostAsJsonAsync("/api/account/Register", user);
            if (response.IsSuccessStatusCode)
            {
                var newUser = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync(), jsonSerializerOptions);
                return newUser;
            }
            else
                return default;
        }
        public async Task LogOut()
        {
             await httpClient.GetAsync("/api/account/Logout");
            
        }
    }
}
