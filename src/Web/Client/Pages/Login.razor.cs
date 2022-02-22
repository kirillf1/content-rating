using Microsoft.AspNetCore.Components;
using Web.Client.Shared;
using Web.Shared;
using Web.Shared.Authentication;

namespace Web.Client.Pages
{
    public partial class Login
    {
        private InputWatcher inputWatcher = default!;
        [Parameter]
        public EventCallback<UserLogin> UserChanged { get; set; }
        private void FieldChanged(string fieldName)
        {
            UserChanged.InvokeAsync(User);
            isInvalid = !inputWatcher.Validate();
        }
        bool isInvalid = true;
        [Inject]
        ApiAuthenticationStateProvider authenticationStateProvider { get; set; } = default!;
        [Inject]
        NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        State state { get; set; } = default!;
        [Inject]
        UserService userService { get; set; } = default!;

        public UserLogin User { get; set; } = default!;
        private string ButtonClass => !isInvalid ? "btn btn-success" : "btn btn-danger";
        protected override void OnInitialized()
        {
            User = new UserLogin("", "");
        }
        private async void HandleValidSubmit()
        {
            state.User = await userService.Login(User.Name,User.Password);
            authenticationStateProvider.NotifyAuthenticationStateChanged();
            navigationManager.NavigateTo(navigationManager.BaseUri);
        }
    }
}
