using Microsoft.EntityFrameworkCore;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class CloseRoomRatingRequest: IRequest<Guid?>
    {
        public string RoomId { get; set; } = default!;
        public int UserId { get; set; }
    }
    public class CloseRoomRatingHandler : IRequestHandler<CloseRoomRatingRequest, Guid?>
    {
        private readonly IRatingDbContext ratingDbContext;

        public CloseRoomRatingHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Checks room Id and creator Id and change room state on completed. If can't room return empty guid
        /// </summary>
        /// <returns>Room Id</returns>

        public async Task<Guid?> HandleAsync(CloseRoomRatingRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(request.RoomId);
            var room = await ratingDbContext.Rooms.FirstOrDefaultAsync(r => r.CreatorId == request.UserId && r.Id == id);
            if (room != null)
            {
                room.IsCompleted = true;
                await ratingDbContext.SaveChangesAsync(default);
                return room.Id;
            }
            return default;
        }
    }
}
