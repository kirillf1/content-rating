using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
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
    public class RoomUpdateHandlerTest
    {
        [Fact]
        public async void Update_Room_Should_Delete_Old_Content_With_Rating()
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
            
            var newContent = room.Contents.Select(c=> new ContentDTO() { Id = c.Id, Name = c.Name, Url = c.Url}).ToList();
            var removedContent = newContent.First();
            newContent.Remove(removedContent);
            newContent.Add(new ContentDTO() { Name = "fdfs", Url = "fdfsf" });
            newContent.Add(new ContentDTO() { Name = "fdfs1", Url = "fdfsf1" });
            newContent.Add(new ContentDTO() { Name = "fdfs2", Url = "fdfsf2" });
            Assert.NotNull(context.UserContentRatings.FirstOrDefault(c => c.ContentId == removedContent.Id));
            
            var updateRoomHandler = new UpdateRoomHandler(context);
            var res = await updateRoomHandler.HandleAsync(new UpdateRoomRequest { UserId = room.CreatorId,RoomId = room.Id.ToString(),Room = new RoomForUpdate {Contents = newContent, Name = "test" } }, default);
            var changedRoom = context.Rooms.Include(c => c.Contents).First(c => room.Id == c.Id);
            Assert.True(res);
            Assert.True(newContent.Count == changedRoom.Contents.Count);
            Assert.Empty(context.Rooms.Where(r => r.Name == "testupdate"));
            Assert.Empty(context.UserContentRatings.Where(c => removedContent.Id == c.ContentId));
        }
    }
}
