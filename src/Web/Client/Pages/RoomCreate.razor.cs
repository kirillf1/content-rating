using Web.Shared.Rooms;
using Web.Shared;
using Microsoft.AspNetCore.Components;
using Web.Client.Shared;

namespace Web.Client.Pages
{
    public partial class RoomCreate
    {
        [Parameter]
        public string? Id { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        RoomService roomService { get; set; } = default!;
        [Inject]
        State State { get; set; } = default!;
        private InputWatcher inputWatcher = default!;
        [Parameter]
        public EventCallback<Room> RoomChanged { get; set; }
        private bool IsLoading { get; set; } = false;
        private void FieldChanged(string fieldName)
        {
            RoomChanged.InvokeAsync(Room);
            isInvalid = !inputWatcher.Validate();
        }
        private string RoomHeader { get => Id == null ? "Создание комнаты" : "Изменить комнату"; }
        bool isInvalid = true;
        public Room Room { get; set; } = default!;
        protected override async Task OnInitializedAsync()
        {
            if (Id == null)
                Room = new Room(string.Empty);
            else
            {
                IsLoading = true;
                Room = await roomService.GetRoom(Id) ?? new Room();
                IsLoading = false;
            }
        }
        private void AddUser()
        {
            Room.Users.Add(new User { Name = "" });
        }
        private void AddContent()
        {
            Room.Contents.Add(new Content());
        }
        private void RemoveContent(Content contentCreate)
        {
            Room.Contents.Remove(contentCreate);
        }
        private void RemoveUser(User user)
        {
            Room.Users.Remove(user);
        }
       
        private async void HandleValidSubmit()
        {
            IsLoading = true;
            if (Id != null)
            {
                var result = await roomService.UpdateRoom(Id, State.User!.Id, Room);
            }
            else
            {
                Id = await roomService.CreateRoom(State.User!.Id, Room);
            }
            IsLoading = false;
            navigationManager.NavigateTo("/RatingRoom/" + Id);
        }
    }
}
