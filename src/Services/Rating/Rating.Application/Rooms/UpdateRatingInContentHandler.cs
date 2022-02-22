using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{

    public class UpdateRatingInContentHandler : IRequestHandler<ChangedContentRating, ChangedContentRating>
    {
        private readonly IRatingDbContext ratingDbContext;

        public UpdateRatingInContentHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Find in UserContentRating by key and change rating
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ChangedContentRating> HandleAsync(ChangedContentRating request, CancellationToken cancellationToken)
        {
            var contentForChange = await ratingDbContext.UserContentRatings.
                SingleAsync(c => c.UserId == request.UserId && c.ContentId == request.ContentId);
            contentForChange.Rating = request.Rating;
            await ratingDbContext.SaveChangesAsync(cancellationToken);
            return request;
        }
    }
}
