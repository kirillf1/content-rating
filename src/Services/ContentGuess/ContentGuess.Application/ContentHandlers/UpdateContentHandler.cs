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
    public class UpdateContentRequest : IRequest<Content>
    {
        
        public UpdateContentRequest(long contentId,ContentWrite contentWrite)
        {
            ContentId = contentId;
            Content = contentWrite;
        }
        public long ContentId { get; set; }
        public ContentWrite Content { get; set; }
    }
    public class UpdateContentHandler : IRequestHandler<UpdateContentRequest, Content>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public UpdateContentHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<Content> HandleAsync(UpdateContentRequest request, CancellationToken cancellationToken)
        {
            var content = await contentGuessDbContext.Contents.Include(t => t.Tags).Include(c=>c.ContentInfo).SingleAsync(c => c.Id == request.ContentId);
            content.ContentTypeId = request.Content.ContentTypeId;
            content.ContentInfo.Url = request.Content.Url;
            content.Name = request.Content.Name;
            content.ContentInfo.StartTimeSeconds = request.Content.ContentStartSeconds;
            if(request.Content.AuthorId.HasValue && request.Content.AuthorId.Value>0)
            content.ContentInfo.AuthorId = request.Content.AuthorId;
            if (request.Content.TagIds != null && request.Content.TagIds.Count > 0)
            {
                content.Tags.Clear();
                var tags = contentGuessDbContext.Tags.Where(t => request.Content.TagIds.Contains(t.Id));
                content.AddTags(tags);
            }
            await contentGuessDbContext.SaveChangesAsync(cancellationToken);
            return content;
        }
    }
}
