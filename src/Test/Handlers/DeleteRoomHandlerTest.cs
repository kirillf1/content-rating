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
    public class DeleteRoomHandlerTest
    {
        [Fact]
        public async void Delete_Room_Should_Clear_UserContentRating()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var room = new Room(Guid.NewGuid(), "testupdate", false);
            room.AddUsers(UserFactory.CreateUsers(4));
            room.AddContent(ContentFactory.CreateContent(10));
            var userForDelete = room.Users.First();
            room.Contents.ForEach(c => room.Users.ForEach(u => u.RatedContent.Add(new UserContentRating(u, c, 0))));
            context.Add(room);
            await context.SaveChangesAsync();
            room.CreatorId = room.Users.First().Id;
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var deleteRoomHandler = new DeleteRoomHandler(context);
            await deleteRoomHandler.HandleAsync(new DeleteRoomRequest(room.Id.ToString(), room.CreatorId), default);

            Assert.Empty(context.UserContentRatings.Where(c => c.ContentId == room.Contents.First().Id).ToArray());
            Assert.True(context.Rooms.FirstOrDefault(r => r.Id == room.Id) == null);
            Assert.True(context.Users.FirstOrDefault(u => u.Id == room.CreatorId) != null);
        }
    }
}
