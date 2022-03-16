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
    public class GetTagListRequest : IRequest<List<TagRead>>
    {

    }
    public class GetTagListQueryHandler : IRequestHandler<GetTagListRequest, List<TagRead>>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public GetTagListQueryHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<List<TagRead>> HandleAsync(GetTagListRequest request, CancellationToken cancellationToken)
        {
            return await contentGuessDbContext.Tags.AsNoTracking().Select(t=>new TagRead(t.Id,t.Name,t.ParentTagId)).ToListAsync();
        }
    }
}
