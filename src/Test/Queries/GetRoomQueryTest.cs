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

namespace Test.Queries
{
    public class GetRoomQueryTest
    {
        [Fact]
        public async void Get_User_Private_Rooms()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var room = new Room(Guid.NewGuid(), "testQuery2", false);
            room.IsPrivate = true;
            var users = UserFactory.CreateUsers(4);
            room.AddUsers(users);
            var room1 = new Room(Guid.NewGuid(), "testQuery", false);
            room1.AddUsers(users);
            room1.IsPrivate = true;
            context.Rooms.AddRange(room,room1);
            var user = room.Users.First();
            await context.SaveChangesAsync();
            room.CreatorId = user.Id;
            room1.CreatorId = user.Id;
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var roomListQueryHandler = new GetRoomListQueryHandler(context);
            var rooms = await roomListQueryHandler.HandleAsync(new GetRoomListQuery(user.Id, RoomType.Personal, 10, 0),default);

            Assert.Equal(2, rooms.Count);
        }
    }
}
