using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace Web.Shared.Rooms
{
    record RoomForCreate(int UserId, string Name, IEnumerable<User> Users, List<Content> Contents,bool IsSingleRoom);
    record RoomForUpdate(int UserId,string RoomId, Room Room);
    public class RoomService
    {
        private readonly HttpClient httpClient;

        public RoomService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> CreateRoom(int userId,Room createRoom)
        {
            var roomForCreate = new RoomForCreate(userId, createRoom.Name, createRoom.Users, createRoom.Contents, createRoom.IsSingleRoom);
            var response = await httpClient.PostAsJsonAsync("/api/rooms", roomForCreate);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<bool> UpdateRoom(string id, int userId, Room updateRoom)
        {
            
            var response = await httpClient.PutAsJsonAsync("/api/rooms/" + id, new RoomForUpdate(userId,id,updateRoom), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response.IsSuccessStatusCode;
        }
        public async Task<Room?> GetRoom(string id)
        {
            return await httpClient.GetFromJsonAsync<Room>("/api/rooms/" + id);
        }
    }
}
