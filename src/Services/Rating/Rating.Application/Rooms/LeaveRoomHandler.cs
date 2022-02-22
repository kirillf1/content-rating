using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class LeaveRoomRequest : IRequest<UserDTO>
    {
        public LeaveRoomRequest(int userId,string roomId)
        {
            UserId = userId;
            RoomId = roomId;
        }
        public int UserId { get; set; }
        public string RoomId { get; set; }
    }
    public class LeaveRoomHandler : IRequestHandler<LeaveRoomRequest, UserDTO>
    {
        private readonly IRatingDbContext ratingDbContext;

        public LeaveRoomHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Search user in room and delete user from this room with rated content
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Released user</returns>
        public async Task<UserDTO> HandleAsync(LeaveRoomRequest request, CancellationToken cancellationToken)
        {
            Guid roomId;
            Guid.TryParse(request.RoomId, out roomId);
            var room = await ratingDbContext.Rooms.Include(u=>u.Users).SingleAsync(c => c.Id == roomId);
            var userForLeave = await ratingDbContext.Users.Include(u => u.RatedContent)
                .SingleAsync(u => u.Id == request.UserId);
            room.DeleteUser(userForLeave.Id);
            ratingDbContext.UserContentRatings.RemoveRange(userForLeave.RatedContent);
            await ratingDbContext.SaveChangesAsync(cancellationToken);

            return new UserDTO( userForLeave);
        }
    }
}
