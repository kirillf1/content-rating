using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.AuthorHandlers.Decorators
{
    public abstract class CacheAuthorDecorator
    {
        protected const string AUTHORS_CACHE = "Authors_Cache";
        protected IMemoryCache memoryCache;
        public CacheAuthorDecorator(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }
    }
}
