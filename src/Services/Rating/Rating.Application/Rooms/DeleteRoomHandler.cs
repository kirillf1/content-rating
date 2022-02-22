using Microsoft.EntityFrameworkCore;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class DeleteRoomRequest : IRequest<bool>
    {
        
        public int UserId { get; set; }
        public string RoomId { get; set; }

        public DeleteRoomRequest(string roomId, int userId)
        {
            RoomId = roomId;
            UserId = userId;
        }
    }
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomRequest, bool>
    {
        private readonly IRatingDbContext ratingDbContext;

        public DeleteRoomHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Checks room Id and creator Id and delete room with UserContentRatings
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> HandleAsync(DeleteRoomRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(request.RoomId);
            var room = await ratingDbContext.Rooms.Include(c=>c.Users).ThenInclude(c=>c.RatedContent).FirstOrDefaultAsync(c=>c.Id == id && c.CreatorId == request.UserId,cancellationToken);
            if (room == null)
                return false;
            room.Users.ForEach(c => ratingDbContext.UserContentRatings.RemoveRange(c.RatedContent));
            ratingDbContext.Rooms.Remove(room);
            
            await ratingDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    } 
    
}
