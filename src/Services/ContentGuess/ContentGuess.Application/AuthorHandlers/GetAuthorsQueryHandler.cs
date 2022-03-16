using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.AuthorHandlers
{
    public class GetAuthorsQueryRequest : IRequest<Author>
    {
        public int Size { get; set; }
        public string? StartWith { get; set; }
    }
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQueryRequest, List<Author>>
    {
        private readonly IContentGuessDbContext context;

        public GetAuthorsQueryHandler(IContentGuessDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Author>> HandleAsync(GetAuthorsQueryRequest request, CancellationToken cancellationToken)
        {
            var query = context.Author.AsNoTracking();
            if(request.StartWith != null)
               query = query.Where(c=>c.Name.StartsWith(request.StartWith));
            query = query.OrderBy(c => c.Name);
            if(request.Size > 0)
              query=  query.Take(request.Size);
            return await query.ToListAsync();
                
             
        }
    }
}
