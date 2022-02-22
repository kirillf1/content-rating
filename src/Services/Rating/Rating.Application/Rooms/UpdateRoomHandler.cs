using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class UpdateRoomRequest : IRequest<bool>
    {
        public string RoomId { get; set; } = default!;
        public int UserId { get; set; }
        public RoomForUpdate Room { get; set; } = default!;
      
        
    }
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomRequest, bool>
    {
        private readonly IRatingDbContext ratingDbContext;

        public UpdateRoomHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Checks roomId and creator Id. If room was found change room with content
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>If success returns true</returns>
        public async Task<bool> HandleAsync(UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.Parse( request.RoomId);
            var room = await ratingDbContext.Rooms.Include(u=>u.Users).ThenInclude(u=>u.RatedContent).Include(c=>c.Contents).
                FirstOrDefaultAsync(c => c.Id == id && c.CreatorId == request.UserId,cancellationToken);
            if(room != null)
            {
                room.IsSingleRoom = request.Room.IsSingleRoom;
                room.Name = request.Room.Name;
                room.IsPrivate = request.Room.IsPrivate;
                room.IsCompleted = request.Room.IsCompleted;
                foreach (var content in request.Room.Contents)
                {
                    var existingContent = room.Contents.FirstOrDefault(c => c.Id == content.Id && c.Id != 0);
                    if (existingContent == null)
                    {
                        var newContent = new Content(content.Url) { Name = content.Name };
                        room.Users.ForEach(u => u.RatedContent.Add(new UserContentRating(u, newContent,0)));
                        room.AddContent(newContent);
                    }
                    else
                    {
                        existingContent.Name = content.Name;
                        existingContent.Url = content.Url;
                    }
                }
                var contentforDelete = room.Contents.Where(c=>c.Id != 0).Where(c => !request.Room.Contents.Any(r => r.Id == c.Id)).ToList();
                foreach ( var content in contentforDelete)
                {
                    ratingDbContext.UserContentRatings.RemoveRange(ratingDbContext.UserContentRatings.Where(c => c.ContentId == content.Id));
                }
                room.DeleteContent(contentforDelete);
                await ratingDbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
        }
    }

}
