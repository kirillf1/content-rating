using Microsoft.AspNetCore.Components;
using Web.Shared.Rooms;

namespace Web.Client.Shared
{
    public partial class RoomViewItem
    {
        [Parameter]
        public RoomPresent Room { get; set; } = default!;
        [Parameter]
        public bool CanEdit { get; set; }
        [Parameter]
        public EventCallback<Guid> DeleteRoom { get; set; }
        [Parameter]
        public EventCallback<RoomPresent> UpdateRoom { get; set; }
        [Parameter]
        public EventCallback<RoomPresent> JoinRoom { get; set; }
        private string RoomState { get => Room.IsCompleted ? "Завершена" : "В процессе"; }
        private async void OnJoinRoom()
        {
           await JoinRoom.InvokeAsync(Room);
        }
        private async void OnDeleteRoom()
        {
            await DeleteRoom.InvokeAsync(Room.Id);
        }
        private async void OnUpdateRoom()
        {
            await UpdateRoom.InvokeAsync(Room);
        }
    }
}
