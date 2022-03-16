using ContentGuess.Application.Dto;
using ContentGuess.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.TagHandlers.Decorators
{
    public class GetTagListDecorator : IRequestHandler<GetTagListRequest, List<TagRead>>
    {
        private const string CACHE_NAME = "Tags_Cache";
        private readonly IRequestHandler<GetTagListRequest, List<TagRead>> handler;
        private readonly IMemoryCache memoryCache;
        
        public GetTagListDecorator(IRequestHandler<GetTagListRequest, List<TagRead>> handler, IMemoryCache memoryCache)
        {
            this.handler = handler;
            this.memoryCache = memoryCache;
        }

        public async Task<List<TagRead>> HandleAsync(GetTagListRequest request, CancellationToken cancellationToken)
        {
            if( memoryCache.TryGetValue<List<TagRead>>(CACHE_NAME, out var list))
            {
                return list;
            }
            else
            {
                list = await handler.HandleAsync(request, cancellationToken);
                memoryCache.Set(CACHE_NAME, list);
                return list;
            }
        }
    }
}
