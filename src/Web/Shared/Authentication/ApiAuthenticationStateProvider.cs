
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Web.Shared.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly State state;
        public UserService userService;
        
        public ApiAuthenticationStateProvider(State state, UserService userService)
        {
            this.state = state;
            this.userService = userService;
            
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await userService.TryAuthorize();
            AuthenticationState stateAuth;
            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            };
                
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                state.User = user;
                stateAuth = new AuthenticationState(new ClaimsPrincipal(id));
            }
            else
            {
                stateAuth = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            return stateAuth;
        }
        public void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
