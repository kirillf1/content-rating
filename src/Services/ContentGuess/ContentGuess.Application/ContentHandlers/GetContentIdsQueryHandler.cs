using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.ContentHandlers
{
    public class GetContentIdsRequest : IRequest<IEnumerable<long>>
    {
       public int ContentSize { get; set; }
       public IEnumerable<int>? TagIds { get; set; }
    }
    public class GetContentIdsQueryHandler : IRequestHandler<GetContentIdsRequest, IEnumerable<long>>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public GetContentIdsQueryHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<IEnumerable<long>> HandleAsync(GetContentIdsRequest request, CancellationToken cancellationToken)
        {
            var query = contentGuessDbContext.Contents.AsQueryable();
            if(request.TagIds !=null && request.TagIds.Any())
            {
                query = query.Include(c => c.Tags).Where(c => c.Tags.Any(t => request.TagIds.Contains(t.Id)));
            }
            return await query.Select(c=>c.Id).OrderBy(c => Guid.NewGuid()).Take(request.ContentSize).ToListAsync();
        }
    }
}
