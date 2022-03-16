using ContentGuess.Application.Dto;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContentGuess.Application.TagHandlers
{
    public class AddTagRequest : IRequest<Tag>
    {
        public AddTagRequest(TagWrite tagWrite)
        {
            Tag = tagWrite;
        }
        public TagWrite Tag { get; set; }
    }
    public class AddTagHandler : IRequestHandler<AddTagRequest, Tag?>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public AddTagHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<Tag?> HandleAsync(AddTagRequest request, CancellationToken cancellationToken)
        {
            if (await contentGuessDbContext.Tags.FirstOrDefaultAsync(t => t.Name == request.Tag.Name) != null)
                return default;
            var tag = new Tag(request.Tag.Name);
             contentGuessDbContext.Tags.Add(tag);
            await contentGuessDbContext.SaveChangesAsync(cancellationToken);
            return tag;
        }
    }
}
