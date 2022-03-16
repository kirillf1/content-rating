using ContentGuess.Application.Dto;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.TagHandlers
{
    public class GetTagRequest : IRequest<TagRead>
    {
        public int TagId { get; set; }
    }
    public class GetTagQueryHandler : IRequestHandler<GetTagRequest, TagRead?>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public GetTagQueryHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<TagRead?> HandleAsync(GetTagRequest request, CancellationToken cancellationToken)
        {
            return await contentGuessDbContext.Tags.Select(t=>new TagRead(t.Id,t.Name,t.ParentTagId)).SingleOrDefaultAsync(t => t.Id == request.TagId);
        }
    }
}
