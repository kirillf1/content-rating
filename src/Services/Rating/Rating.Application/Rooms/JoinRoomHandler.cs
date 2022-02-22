using Microsoft.EntityFrameworkCore;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class JoinRoomRequest: IRequest<Room>
    {
        public string RoomId { get; set; } = default!;
        public int UserId { get; set; }
    }
    public class JoinRoomHandler : IRequestHandler<JoinRoomRequest, Room>
    {
        private readonly IRatingDbContext ratingDbContext;

        public JoinRoomHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Search existing room and user.If room don't contains this user add user in room and create UserContentRating
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Room with content</returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<Room> HandleAsync(JoinRoomRequest request, CancellationToken cancellationToken)
        {
            Guid roomId;
            if (!Guid.TryParse(request.RoomId, out roomId))
                throw new ArgumentException();
            var room = await ratingDbContext.Rooms.Include(r => r.Contents).Include(r => r.Users).SingleAsync(r=>r.Id == roomId,cancellationToken);
            if ((!room.Users.Any(u => u.Id == request.UserId)) && (!room.IsCompleted))
            {
                var newRoomUser = await ratingDbContext.Users.SingleAsync(u => u.Id == request.UserId);
                newRoomUser.RatedContent.AddRange(room.Contents.Select(c => new UserContentRating(newRoomUser, c, 0)));
                room.Users.Add(newRoomUser);
                await ratingDbContext.SaveChangesAsync(cancellationToken);
            }
            //room.Users.ForEach(u => u.Password = "");
            return room;
            
        }
    }
}
