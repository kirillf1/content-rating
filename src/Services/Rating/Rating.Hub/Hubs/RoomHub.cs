using Rating.Application.Rooms;
using Rating.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Rating.Domain;
using Rating.Application.Dto;

namespace Rating.Hub.Hubs
{
    public class RoomHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private const string ReceiveRatingMethod = "ReceiveNewRating";
        private const string UsersUpdatedMethod = "UsersUpdated";
        private const string ReceiveAllRatingMethod = "ReceiveAllRating";
        private const string ReceiveRoomIdMethod = "ReceiveRoomId";
        private const string ReceiveRoomMethod = "ReceiveRoom";
        private const string KickFromRoomMethod = "KickFromRoom";
        private const string EndRoomRatingMethod = "EndRoomRating";

        private const string UserLeaveCommandName = "out";
        private const string UserJoinCommandName = "in";

        private readonly IRequestHandler<GetUsersRatingQuery, IList<UsersRating>> userRatingQueryHandler;
        private readonly IRequestHandler<JoinRoomRequest, Room> joinRoomHandler;
        private readonly IRequestHandler<LeaveRoomRequest, UserDTO> leaveRoomHandler;
        private readonly IRequestHandler<ChangedContentRating, ChangedContentRating> updateRatingHandler;
        private readonly IRequestHandler<CloseRoomRatingRequest, Guid?> closeRoomHandler;

        public RoomHub(IRequestHandler<GetUsersRatingQuery, IList<UsersRating>> userRatingQueryHandler,
            IRequestHandler<JoinRoomRequest, Room> joinRoomHandler, IRequestHandler<LeaveRoomRequest, UserDTO> leaveRoomHandler,
            IRequestHandler<ChangedContentRating, ChangedContentRating> updateRatingHandler, 
            IRequestHandler<CloseRoomRatingRequest, Guid?> closeRoomHandler)
        {
            
            this.userRatingQueryHandler = userRatingQueryHandler;
            this.joinRoomHandler = joinRoomHandler;
            this.leaveRoomHandler = leaveRoomHandler;
            this.updateRatingHandler = updateRatingHandler;
            this.closeRoomHandler = closeRoomHandler;
        }
        /// <summary>
        /// Get room for caller and rating and add this client in group. Then notify other clients in this group
        /// </summary>
        public async Task JoinRoom(string roomId, int userId)
        {
            var room = await joinRoomHandler.HandleAsync(new JoinRoomRequest { RoomId = roomId, UserId = userId },Context.ConnectionAborted);
            if (!room.IsCompleted)
            {
                await Clients.Group(roomId.ToString()).SendAsync(UsersUpdatedMethod,
                    new UserDTO(room.Users.First(u => u.Id == userId)), UserJoinCommandName);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            }
            await Clients.Caller.SendAsync(ReceiveRoomMethod, room, Context.ConnectionAborted);
            await Clients.Caller.SendAsync(ReceiveAllRatingMethod,
                await userRatingQueryHandler.HandleAsync(new GetUsersRatingQuery(roomId),Context.ConnectionAborted));
        }
        /// <summary>
        /// Update rating and send new rating all clients in this group
        /// </summary>
       
        public async Task UpdateRatingContent(string roomId,int userId, long contentId, double rating)
        {
            var content = await updateRatingHandler.HandleAsync(new ChangedContentRating(userId, contentId, rating),Context.ConnectionAborted);
            await Clients.OthersInGroup(roomId).SendAsync(ReceiveRatingMethod,content);

        }
        /// <summary>
        /// Delete user from this room and notify clients in this group
        /// </summary>
       
        public async Task LeaveRoom(string roomId, int userId)
        {
            var releasedUser = await leaveRoomHandler.HandleAsync(new LeaveRoomRequest(userId,roomId),Context.ConnectionAborted);
            await Clients.OthersInGroup(roomId).SendAsync(UsersUpdatedMethod, releasedUser,UserLeaveCommandName,Context.ConnectionAborted);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId, Context.ConnectionAborted);
        }
        /// <summary>
        /// Delete user from room and send to clients who need to exit
        /// </summary>
        
        public async Task KickFromRoom(int userId,string roomId)
        {
            var releasedUser  = await leaveRoomHandler.HandleAsync(new LeaveRoomRequest(userId, roomId),Context.ConnectionAborted);
            await Clients.Group(roomId).SendAsync(UsersUpdatedMethod, releasedUser, UserLeaveCommandName, Context.ConnectionAborted);
            await Clients.OthersInGroup(roomId).SendAsync(KickFromRoomMethod, userId, Context.ConnectionAborted);
        }
        /// <summary>
        /// Change state room and notify all clients in this room of new room state
        /// </summary>
        public async Task EndRating(int userId, string roomId)
        {
            var result = await closeRoomHandler.HandleAsync(new CloseRoomRatingRequest { RoomId = roomId, UserId = userId }, Context.ConnectionAborted);
            if(result != default)
            {
                await Clients.Group(roomId).SendAsync(EndRoomRatingMethod, result.ToString());
            }
        }
    }
}
