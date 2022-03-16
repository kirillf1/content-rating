using ContentGuess.Application.Dto;
using ContentGuess.Application.TagHandlers;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContentGuess.Application.ContentHandlers
{
    public class GetContentRequest : IRequest<ContentWrite>
    {
        public GetContentRequest(long id)
        {
            Id = id;
        }
        public long Id { get; set; }
       
    }
    public class GetContentUpdateQueryHandler : IRequestHandler<GetContentRequest, ContentWrite>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;
        

        public GetContentUpdateQueryHandler(IContentGuessDbContext contentGuessDbContext )
        {
            this.contentGuessDbContext = contentGuessDbContext;
            
        }
        public async Task<ContentWrite> HandleAsync(GetContentRequest request, CancellationToken cancellationToken)
        {
            var c = await contentGuessDbContext.Contents.Include(c => c.Tags).Include(c=>c.ContentInfo).Include(c=>c.ContentType)
                .FirstAsync(c => c.Id == request.Id);
            var content = new ContentWrite(c.Name,c.ContentInfo.Url,c.Tags.Select(c => c.Id).ToList(),c.ContentTypeId,c.ContentInfo.AuthorId,c.ContentInfo.StartTimeSeconds);
          
            return content;
        }

       
    }
}
