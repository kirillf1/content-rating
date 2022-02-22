using Microsoft.EntityFrameworkCore;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class GetRoomQuery: IRequest<Room>
    {
       public Guid RoomId;

        public GetRoomQuery(Guid roomId)
        {
            RoomId = roomId;
        }
    }
    public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Room>
    {
        private readonly IRatingDbContext ratingDbContext;

        public GetRoomQueryHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        public async Task<Room> HandleAsync(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var room = await ratingDbContext.Rooms.Include(c => c.Contents).Include(u => u.Users.Where(uf => uf.UserType == UserType.Fake)).
                SingleAsync(r => r.Id == request.RoomId);
            return room;
        }
    }
}
