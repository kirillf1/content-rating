using Rating.Application.Rooms;
using Rating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helpers.EfHelpers;
using Test.Helpers.ObjectFactories;
using Xunit;

namespace Test.Handlers
{
    public class LeaveRoomHandlerTest
    {
        [Fact]
        public async Task Delete_User_FromRoom_UserContent_ShouldEmpty()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var leaveRoomHandler = new LeaveRoomHandler(context);
            var room = new Room(Guid.NewGuid(), "testLeave", false);
            room.AddUsers(UserFactory.CreateUsers(4));
            room.AddContent(ContentFactory.CreateContent(10));
            var userForDelete = room.Users.First();
            room.Contents.ForEach(c => userForDelete.RatedContent.Add(new UserContentRating(userForDelete, c, 0)));
            context.Rooms.Add(room);
            await context.SaveChangesAsync();

            await leaveRoomHandler.HandleAsync(new LeaveRoomRequest(userForDelete.Id, room.Id.ToString()),default);
            context.ChangeTracker.Clear();
            var rating = context.UserContentRatings.Where(u => u.UserId == userForDelete.Id).ToList();

            Assert.Equal(3, room.Users.Count);
            Assert.Empty(rating);
        }
    }
}
