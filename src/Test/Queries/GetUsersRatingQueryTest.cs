using Rating.Application.Rooms;
using Rating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helpers.EfHelpers;
using Xunit;

namespace Test.Queries
{
    public class GetUsersRatingQueryTest
    {
        [Fact]
        public async Task Get_Content_With_User_Rating_ByCurrentRoom()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var queryHandler = new GetUsersRatingQueryHandler(context);
            var users = new List<User> { new User("kirill"), new User("david"), new User("kantic") };
            var contents = new List<Content> { new Content("test"), new Content("test1"), new Content("test2") };
            users.ForEach(u => contents.ForEach(c => u.RatedContent.Add(new UserContentRating(u, c, 0))));
            
            var room = new Room(Guid.NewGuid(), "testRomm", false);
            room.AddUsers(users);
            room.AddContent(contents);
            context.Rooms.Add(room);
            context.UserContentRatings.Add(new UserContentRating(new User("unknown"),new Content("test3"),10));
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            var result = await queryHandler.HandleAsync(new GetUsersRatingQuery(room.Id.ToString()), default);

            Assert.Equal(3, result.Count);
            foreach (var content in result)
            {
                Assert.Equal(3, content.RatedContent.Count);
            }
        }
    }
}
