using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application.Rooms
{
    public class CreateRoomRequest : IRequest<Guid>
    {
        public CreateRoomRequest()
        {

        }
        public CreateRoomRequest(int userId,string name,bool isSingleRoom,ICollection<UserDTO> users, ICollection<ContentDTO> contents)
        {
            UserId = userId;
            Name = name;
            IsSingleRoom = isSingleRoom;
            Users = users;
            Contents = contents;
        }
        public int UserId { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<UserDTO> Users { get; set; } = default!;
        public ICollection<ContentDTO> Contents { get; set; } = default!;
        public bool IsSingleRoom { get; set; }
    }
    public class CreateRoomHandler : IRequestHandler<CreateRoomRequest, Guid>
    {
        private readonly IRatingDbContext ratingDbContext;

        public CreateRoomHandler(IRatingDbContext ratingDbContext)
        {
            this.ratingDbContext = ratingDbContext;
        }
        /// <summary>
        /// Creates room and for each content and user add UserContentRating with default value
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Room Id</returns>
        public async Task<Guid> HandleAsync(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            Room room = new Room(Guid.NewGuid(), request.Name, request.IsSingleRoom);
            room.CreatorId = request.UserId;
            room.IsCompleted = false;
            List<User> users = new();
            users.Add(await ratingDbContext.Users.SingleAsync(u => u.Id == request.UserId,  cancellationToken));
            if (request.IsSingleRoom)
                // add fake users
                users.AddRange(request.Users.Select(u => new User(u.Name)));
            room.Users.AddRange(users);
            room.AddContent(request.Contents.Select(c => new Content(c.Url) { Name = c.Name}));
            ratingDbContext.Rooms.Add(room);
            foreach (var user in room.Users)
            {
                foreach (var content in room.Contents)
                {
                    user.RatedContent.Add(new UserContentRating(user,content,0));
                }
            }
            await ratingDbContext.SaveChangesAsync(cancellationToken);
            return room.Id;
        }
    }
}
