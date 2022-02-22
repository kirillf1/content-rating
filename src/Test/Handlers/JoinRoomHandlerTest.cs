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
    public class JoinRoomHandlerTest
    {
        [Fact]
        public async Task Add_NewUser_InRoom()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var joinRooomHandler = new JoinRoomHandler(context);
            var room = new Room(Guid.NewGuid(), "testJoin", false);
            room.AddUsers(UserFactory.CreateUsers(4));
            room.AddContent(ContentFactory.CreateContent(10));
            context.Rooms.Add(room);
            var newUser = UserFactory.CreateUser();
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            
            context.ChangeTracker.Clear();
            var changedRoom = await joinRooomHandler.HandleAsync(new JoinRoomRequest { RoomId = room.Id.ToString(), UserId = newUser.Id }, default);

            Assert.Equal(room.Id, changedRoom.Id);
            Assert.True(changedRoom.Users.Count > room.Users.Count);
            Assert.Equal(room.Contents.Count, changedRoom.Contents.Count);
            Assert.Contains(changedRoom.Users, u => u.Id == newUser.Id);
        }
        [Fact]
        public async Task Join_Room_ExistingUser_In_Current_Room_Should_Dont_Add_User()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var joinRooomHandler = new JoinRoomHandler(context);
            var room = new Room(Guid.NewGuid(), "testJoin", false);
            room.AddUsers(UserFactory.CreateUsers(4));
            room.AddContent(ContentFactory.CreateContent(10));
            var joiningUser = room.Users.First();
            context.Rooms.Add(room);
            await context.SaveChangesAsync();

            context.ChangeTracker.Clear();
            var changedRoom = await joinRooomHandler.HandleAsync(new JoinRoomRequest { RoomId = room.Id.ToString(), UserId = joiningUser.Id }, default);

            Assert.Equal(room.Users.Count, changedRoom.Users.Count);
            Assert.Equal(room.Id, changedRoom.Id);
            Assert.Equal(room.Contents.Count, changedRoom.Contents.Count);
            Assert.Contains(changedRoom.Users, u => u.Id == joiningUser.Id);

        }
    }
}
