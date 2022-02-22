using Microsoft.AspNetCore.SignalR;
using Rating.Application.Dto;
using Rating.Application.Rooms;
using Rating.Domain.Interfaces;

namespace Rating.Hub.Hubs
{
    public class RoomListHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private const string ReceiveRoomsMethod = "ReceiveRooms";
        private const string RoomDeletedMethod = "RoomDeleted";
        private readonly IRequestHandler<GetRoomListQuery, List<RoomPresent>> roomListQueryHandler;
        private readonly IRequestHandler<DeleteRoomRequest, bool> deleteRoomHandler;

        public RoomListHub(IRequestHandler<GetRoomListQuery, List<RoomPresent>> roomListQueryHandler, 
            IRequestHandler<DeleteRoomRequest, bool> deleteRoomHandler)
        {
            this.roomListQueryHandler = roomListQueryHandler;
            this.deleteRoomHandler = deleteRoomHandler;
        }
        /// <summary>
        /// Parse room type and return room collection by query
        /// </summary>
        /// <returns></returns>
        public async Task GetRooms(int userId,string roomType, int roomCount,int skipCount)
        {
            var roomTypeEnum = Enum.Parse<RoomType>(roomType);
            var rooms = await roomListQueryHandler.HandleAsync(
               new GetRoomListQuery(userId, roomTypeEnum, roomCount, skipCount), Context.ConnectionAborted);
            await Clients.Caller.SendAsync(ReceiveRoomsMethod, rooms,Context.ConnectionAborted);
        }
        /// <summary>
        /// Delete room by Id. if success send room id for all connected clients
        /// </summary>

        public async Task DeleteRoom(int userId, string roomId)
        {
            var res = await deleteRoomHandler.HandleAsync(new DeleteRoomRequest(roomId, userId), Context.ConnectionAborted);
            if (res)
            {
                await Clients.All.SendAsync(RoomDeletedMethod, roomId);
            }
        }
    }
}
