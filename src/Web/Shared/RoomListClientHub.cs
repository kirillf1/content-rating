using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Shared.Rooms;

namespace Web.Shared
{
    public class RoomListClientHub : IAsyncDisposable
    {

        private HubConnection? _hubConnection;
        private readonly string hubUrl;
        private bool isInit;
        public event Action<List<RoomPresent>>? RoomsUpdated;
        public event Action<string>? RoomDeleted;
        public RoomListClientHub(string hubUrl)
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
                _hubConnection.On<List<RoomPresent>>("ReceiveRooms", r => RoomsUpdated?.Invoke(r));
                _hubConnection.On<string>("RoomDeleted", r => RoomDeleted?.Invoke(r));
                await _hubConnection.StartAsync();
                isInit = true;
            }
        }

        public async Task GetRooms(int userId, string roomType, int roomCount, int skipCount)
        {
           await _hubConnection!.SendAsync("GetRooms", userId, roomType, roomCount, skipCount);
        }
        public async Task DeleteRoom(int userId, string roomId)
        {
            await _hubConnection!.SendAsync("DeleteRoom", userId, roomId);
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
