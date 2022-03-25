using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.AuthorHandlers.Decorators
{
    public class GetAuthorsQueryCacheDecorator :CacheAuthorDecorator, IRequestHandler<GetAuthorsQueryRequest, List<Author>>
    {
        private readonly IRequestHandler<GetAuthorsQueryRequest, List<Author>> handler;
        

        public GetAuthorsQueryCacheDecorator(IRequestHandler<GetAuthorsQueryRequest, List<Author>> handler,IMemoryCache memoryCache) : base(memoryCache)
        {
            this.handler = handler;
            
        }

        public async Task<List<Author>> HandleAsync(GetAuthorsQueryRequest request, CancellationToken cancellationToken)
        {
           if(memoryCache.TryGetValue(AUTHORS_CACHE, out List<Author> Authors))
            {
                return Authors.OrderBy(a=>a.Name).ToList();
            }
            else
            {
                Authors = await handler.HandleAsync(request, cancellationToken);
                memoryCache.Set(AUTHORS_CACHE, Authors,TimeSpan.FromHours(2));
                return Authors;
            }
        }
    }
}
