using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class GetRoomListQuery : IRequest<List<RoomPresent>>
    {
        public GetRoomListQuery(int userId, RoomType roomType, int roomCount, int skipCount)
        {
            UserId = userId;
            RoomType = roomType;
            RoomCount = roomCount;
            SkipCount = skipCount;
        }
        public int UserId { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomCount { get; set; }
        public int SkipCount { get; set; }
    }
    public class GetRoomListQueryHandler : IRequestHandler<GetRoomListQuery, List<RoomPresent>>
    {
        private readonly IRatingDbContext ratingDbContext;

        public GetRoomListQueryHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Returns room dto collection depend on room type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<RoomPresent>> HandleAsync(GetRoomListQuery request, CancellationToken cancellationToken)
        {
            var query = ratingDbContext.Rooms.Include(u=>u.Users).AsNoTracking();
            
            switch (request.RoomType)
            {
                case RoomType.Personal:
                    query = query.Where(c=>c.CreatorId == request.UserId);
                    break;
                case RoomType.Public:
                    query = query.Where(r=>(r.CreatorId != request.UserId) && r.IsPrivate == false);
                    break;
                case RoomType.Member:
                    query = query.Where(r => r.Users.Any(c => c.Id == request.UserId) && r.CreatorId != request.UserId);
                    break;
            }
            var rooms = await query.Skip(request.SkipCount).Take(request.RoomCount).OrderByDescending(c=>c.CreationTime)
                .Select(r => new RoomPresent(r.Id, r.Name, r.CreationTime, r.IsCompleted)).ToListAsync();
            
            return rooms;
        }
    }
    public enum RoomType
    {
        Personal,
        Public,
        Member

    }
}
