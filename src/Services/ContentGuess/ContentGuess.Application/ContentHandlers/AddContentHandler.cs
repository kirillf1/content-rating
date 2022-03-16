using ContentGuess.Application.Dto;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.ContentHandlers
{
    public class AddContentRequest : IRequest<Content>
    {
        public AddContentRequest(List<ContentWrite> contentWrite)
        {
            this.Content = contentWrite;
        }
        public List<ContentWrite> Content{ get; set; }

    }
    public class AddContentHandler : IRequestHandler<AddContentRequest, List<Content>>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public AddContentHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<List<Content>> HandleAsync(AddContentRequest request, CancellationToken cancellationToken)
        {
            var contentForAdd = new List<Content>();
            foreach (var item in request.Content)
            {
                IEnumerable<Tag> tags = Enumerable.Empty<Tag>();
                if (item.TagIds != null && item.TagIds.Count > 0)
                    tags = await contentGuessDbContext.Tags.Include(c => c.Content).Where(t => item.TagIds.Contains(t.Id)).ToArrayAsync(cancellationToken);
                var contentInfo = new ContentInfo(item.Url);
                if (item.AuthorId.HasValue)
                    contentInfo.AddAuthor(await contentGuessDbContext.Author.FirstAsync(a => a.Id == item.AuthorId.Value));
                
                contentInfo.StartTimeSeconds = item.ContentStartSeconds;
                var content = new Content(item.Name, await contentGuessDbContext.ContentType.FirstAsync(c=>c.Id == item.ContentTypeId), contentInfo, tags);
                
                contentForAdd.Add(content);
            }
            contentGuessDbContext.Contents.AddRange(contentForAdd);
            await contentGuessDbContext.SaveChangesAsync(cancellationToken);
            return contentForAdd;
        }
    }
}
