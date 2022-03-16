using ContentGuess.Application.Dto;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.TagHandlers
{
    public class UpdateTagRequest : IRequest<Tag>
    {
        public UpdateTagRequest(int tagId, TagWrite tag)
        {
            Tag = tag;
            TagId = tagId;
        }
        public TagWrite Tag { get; set; }
        public int TagId { get; set; }
    }
    public class UpdateTagHandler : IRequestHandler< UpdateTagRequest,Tag>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public UpdateTagHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<Tag> HandleAsync(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            var tag =  await contentGuessDbContext.Tags.SingleAsync(t => t.Id == request.TagId);
            tag.Name = request.Tag.Name;
            await contentGuessDbContext.SaveChangesAsync(cancellationToken);
            return tag;
        }
    }
}
