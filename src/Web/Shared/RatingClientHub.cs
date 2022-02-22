using Microsoft.AspNetCore.SignalR.Client;
using Web.Shared.Rating;
using Web.Shared.Rooms;

namespace Web.Shared
{
    public class RatingClientHub : IAsyncDisposable
    {
        private HubConnection? _hubConnection;
        private readonly string hubUrl;
        private bool isInit;
        public event Action<ContentWithRating>? ContentRatingChanged;
        public event Action<User,string>? UsersChanged;
        public event Action<List<UsersRating>>? ContentUsersLoaded;
        public event Action<Room>? RoomLoaded;
        public event Action<string>? RoomEstimated;
        public event Action<int>? UserKicked;
        public RatingClientHub(string hubUrl)
        {
            this.hubUrl = hubUrl;
            isInit = false;
        }
        public async Task Init()
        {
            if (!isInit)
            {
                _hubConnection = new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .Build();
                _hubConnection.On<ContentWithRating>("ReceiveNewRating", c => ContentRatingChanged?.Invoke(c));
                _hubConnection.On<User,string>("UsersUpdated", (u,c) => UsersChanged?.Invoke(u,c));
                _hubConnection.On<List<UsersRating>>("ReceiveAllRating", c => ContentUsersLoaded?.Invoke(c));
                _hubConnection.On<Room>("ReceiveRoom", r => RoomLoaded?.Invoke(r));
                _hubConnection.On<int>("KickFromRoom", u => UserKicked?.Invoke(u));
                _hubConnection.On<string>("EndRoomRating", id => RoomEstimated?.Invoke(id));
                await _hubConnection.StartAsync();
                isInit = true;
            }
        }
        public async Task UpdateRatingContent(string roomId, int userId, long contentId, double rating)
        {
            await _hubConnection!.SendAsync(nameof(UpdateRatingContent), roomId, userId, contentId, rating);
        }
        public async Task JoinRoom(string roomId,int userId)
        {
           await _hubConnection!.SendAsync(nameof(JoinRoom), roomId, userId);
        }
        public async Task LeaveRoom(string roomId,int userId)
        {
            await _hubConnection!.SendAsync(nameof(LeaveRoom), roomId, userId);
        }
        public async Task KickFromRoom(int userId, string roomId)
        {
            await _hubConnection!.SendAsync(nameof(KickFromRoom), userId, roomId);
        }
        public async Task EndRating(int userId, string roomId)
        {
            await _hubConnection!.SendAsync(nameof(EndRating), userId, roomId);
        }
        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
                isInit = false;
            }
        }

    }
}
