using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Web.Shared;
using Web.Shared.Rooms;

namespace Web.Client.Pages
{
    public partial class RoomList
    {
        const string PersonalRoomType = "Personal";
        const string PublicRoomType = "Public";
        const string MemberRoomType = "Member";
        private string RoomType { get; set; } = default!;
        [Inject]
        IJSRuntime JSRuntime { get; set; } = default!;
        [Inject]
        NavigationManager navigationManager { get; set; } = default!;
        [Inject]
        private RoomListClientHub RoomListClientHub { get; set; } = default!;
        [Inject]
        State State { get; set; } = default!;
        private List<RoomPresent> Rooms { get; set; } = new List<RoomPresent>();
        private bool CanEdit { get; set; }
        private bool IsLoading { get; set; } = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RoomListClientHub.Init();
                if (State.User == null)
                {
                    RoomType = PublicRoomType;
                    await RoomListClientHub.GetRooms(0, PublicRoomType, 100, 0);
                }
                else
                {
                    RoomType = PersonalRoomType;
                    await RoomListClientHub.GetRooms(State.User.Id, PersonalRoomType, 100, 0);
                    CanEdit = true;
                }
                IsLoading = true;
            }
        }
        protected override void OnInitialized()
        {
            RoomListClientHub.RoomsUpdated += RoomListClientHub_RoomsUpdated;
            RoomListClientHub.RoomDeleted += RoomListClientHub_RoomDeleted;
        }
        public void UpdateRoom(RoomPresent room)
        {
            navigationManager.NavigateTo("/createroom/" + room.Id);
        }
        public async void JoinRoom(RoomPresent room)
        {
            if((State.User?.Id ?? 0) <= 0 && !room.IsCompleted) 
            {
                await JSRuntime.InvokeVoidAsync("alert", "Для входа нужно авторизироваться!");
                return;
            }
            if (room.IsCompleted)
                navigationManager.NavigateTo("/resultroom/" + room.Id);
            else
            navigationManager.NavigateTo("/ratingroom/" + room.Id);
        }
        public async void DeleteRoom(Guid roomId)
        {
            IsLoading = true;
            await RoomListClientHub.DeleteRoom(State.User!.Id, roomId.ToString());

        }
        private void RoomListClientHub_RoomDeleted(string roomId)
        {
            var id = Guid.Parse(roomId);
            var roomforDelete = Rooms.FirstOrDefault(r=>r.Id == id);
            if (roomforDelete != null) 
            {
                Rooms.Remove(roomforDelete);
                IsLoading = false;
                StateHasChanged();
            }
        }

        private async Task RoomTypeChanged(string value)
        {
           
            if (value == RoomType)
                return;
            Rooms.Clear();
            switch (value)
            {
                case PersonalRoomType:
                    await RoomListClientHub.GetRooms(State.User!.Id, PersonalRoomType, 100, 0);
                    CanEdit = true;
                    break;
                case PublicRoomType:
                    await RoomListClientHub.GetRooms(State.User?.Id ?? 0 , PublicRoomType, 100, 0);
                    CanEdit = false;
                    break;
                case MemberRoomType:
                    await RoomListClientHub.GetRooms(State.User!.Id, MemberRoomType, 100, 0);
                    CanEdit = false;
                    break;
            }
            RoomType = value!;
        }
        public async ValueTask DisposeAsync()
        {
            RoomListClientHub.RoomsUpdated -= RoomListClientHub_RoomsUpdated;
            RoomListClientHub.RoomDeleted -= RoomListClientHub_RoomDeleted;
            await RoomListClientHub.DisposeAsync();
        }
        private void RoomListClientHub_RoomsUpdated(List<RoomPresent> rooms)
        {

            Rooms = rooms;
            IsLoading = false;
            StateHasChanged();
        }
    }
}
