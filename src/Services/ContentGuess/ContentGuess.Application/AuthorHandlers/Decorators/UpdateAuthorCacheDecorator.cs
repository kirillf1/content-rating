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
    public class UpdateAuthorChacheDecorator<T> : CacheAuthorDecorator, IRequestHandler<T, Author>
    {
        private readonly IRequestHandler<T, Author> handler;

        public UpdateAuthorChacheDecorator(IRequestHandler<T, Author> handler, IMemoryCache memoryCache) : base(memoryCache)
        {
            this.handler = handler;
        }

        public async Task<Author> HandleAsync(T request, CancellationToken cancellationToken)
        {
            var author = await handler.HandleAsync(request, cancellationToken);
            if (memoryCache.TryGetValue(AUTHORS_CACHE, out List<Author> authors))
            {
                var updatedAuthor = authors.Find(a => a.Id == author.Id);
                if (updatedAuthor != null)
                    updatedAuthor.Name = author.Name;
            }
            return author;
        }
    }
}
