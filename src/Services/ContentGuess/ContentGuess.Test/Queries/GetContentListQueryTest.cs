using ContentGuess.Application.ContentHandlers;
using ContentGuess.Domain;
using ContentGuess.Test.Helpers.EfHelpers;
using ContentGuess.Test.Helpers.ObjectFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ContentGuess.Test.Queries
{
    public class GetContentListQueryTest
    {
        [Fact]
        public async void Get_Content_Collection_Sorted_By_Name()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var contentCollection = ContentFactory.CreateContent(5);
            contentCollection[0].Name = "E";
            contentCollection[1].Name = "B";
            contentCollection[2].Name = "C";
            contentCollection[3].Name = "D";
            contentCollection[4].Name = "A";
            context.Contents.AddRange(contentCollection);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var contentHandler = new GetContentListQueryHandler(context);
            var result = await contentHandler.HandleAsync(new ContentListQuery(5) { OrderColumn = "name" }, default);

            Assert.Equal(5, result.Count);
            Assert.Equal("A", result.First().Name);
        }
        [Fact]
        public async void Get_Content_Sorted_By_Tag()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var contentCollection = ContentFactory.CreateContent(5);
            var tag = new Tag("Anime");
            contentCollection[0].AddTag(tag);
            contentCollection[1].AddTag(tag);
            context.Contents.AddRange(contentCollection);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var contentHandler = new GetContentListQueryHandler(context);
            var result = await contentHandler.HandleAsync(new ContentListQuery(5) { TagIds = new List<int> {tag.Id } }, default);

            Assert.Contains(contentCollection, c => c.Name == contentCollection[0].Name);
            Assert.True(result.Count == 2);

        }
    }
}
