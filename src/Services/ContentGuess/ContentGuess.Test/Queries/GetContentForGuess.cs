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
    public class GetContentForGuess
    {
        [Fact]
        public async void Get_Content_With_FalseNames()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var contentCollection = ContentFactory.CreateContent(5);
            context.AddRange(contentCollection);
            context.SaveChanges();
            context.ChangeTracker.Clear();

            var queryHandler = new GetContentListForGuessHandler(new GetContentListQueryHandler(context),context);
            var result = await queryHandler.HandleAsync(new ContentListForGuessQuery(5,4), default);
            
            Assert.Equal(5,result.Count);
            foreach (var content in result)
            {
                Assert.Equal(4, content.FalseNames.Count);
                
            }
        }
        [Fact]
        public async void Get_Content_With_FalseNames_ByTag()
        {
            using var context = ContextFactory.CreateInMemoryContext();
            var contentCollection = ContentFactory.CreateContent(6);
            var tags = new List<Tag> { new Tag("anime"), new Tag("naruto") };
            contentCollection.First().Tags.Clear();
            contentCollection.ForEach(content => content.AddTags(tags));
            context.AddRange(contentCollection);
            context.SaveChanges();
            context.ChangeTracker.Clear();

            var queryHandler = new GetContentListForGuessHandler(new GetContentListQueryHandler(context), context);
            var result = await queryHandler.HandleAsync(new ContentListForGuessQuery(5, 4) { TagIds= new List<int> { tags.First().Id } }, default);

            Assert.Equal(5, result.Count);
            foreach (var item in result)
            {
                Assert.Equal(4, item.FalseNames.Count);
                
            }
        }
    }
}
