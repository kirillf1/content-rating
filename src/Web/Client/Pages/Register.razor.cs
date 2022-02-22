using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Web.Client.Shared;
using Web.Shared;
using Web.Shared.Authentication;

namespace Web.Client.Pages
{
    public partial class Register
    {
        private InputWatcher inputWatcher = default!;
        [Parameter]
        public EventCallback<UserRegister> UserChanged { get; set; }
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
       
        public UserRegister User { get; set; } = default!;
        private string ButtonClass => !isInvalid ? "btn btn-success" : "btn btn-danger";
        protected override void OnInitialized()
        {
            User = new UserRegister("", "", "");
        }
        private async void HandleValidSubmit()
        {
            state.User = await userService.Register(User);
            authenticationStateProvider.NotifyAuthenticationStateChanged();
            navigationManager.NavigateTo(navigationManager.BaseUri);
        }
    }
}
