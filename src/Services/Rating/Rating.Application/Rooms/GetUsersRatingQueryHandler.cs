using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class GetUsersRatingQuery: IRequest<IList<UsersRating>>
    {
        public GetUsersRatingQuery()
        {

        }
        public GetUsersRatingQuery(string roomId)
        {
            RoomId = roomId;
        }
        public string? RoomId { get; set; } = default!;
    }
    public class GetUsersRatingQueryHandler : IRequestHandler<GetUsersRatingQuery, IList<UsersRating>>
    {
        private readonly IRatingDbContext ratingDbContext;

        public GetUsersRatingQueryHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        public async Task<IList<UsersRating>> HandleAsync(GetUsersRatingQuery request, CancellationToken cancellationToken)
        {
            Guid roomId;
            if (!Guid.TryParse(request.RoomId, out roomId))
                throw new ArgumentException();
            var roomUser = await ratingDbContext.Rooms.AsNoTracking().Include(r => r.Users).Select(r => new { r.Id, r.Users }).SingleAsync(r => r.Id == roomId);
            var usersRating = await ratingDbContext.UserContentRatings.AsNoTracking().Where(r => roomUser.Users.Select(u => u.Id).Contains(r.UserId)).GroupBy(c => c.ContentId).
                Select(c => new UsersRating(c.Key, c.Select(u => new RatedContent(u.UserId, u.Rating)))).ToListAsync();
            return usersRating;
        }
    }
}
