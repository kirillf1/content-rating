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
    public class AddAuthorRequest : IRequest<Author>
    {
        public AddAuthorRequest(Author author)
        {
            Author = author;
        }
       
        public Author Author { get; set; }
    }
    public class AddAuthorHandler : IRequestHandler<AddAuthorRequest, Author>
    {
        private readonly IContentGuessDbContext contentGuessDb;

        public AddAuthorHandler(IContentGuessDbContext contentGuessDb)
        {
            this.contentGuessDb = contentGuessDb;
        }
        public async Task<Author> HandleAsync(AddAuthorRequest request, CancellationToken cancellationToken)
        {
            if(await contentGuessDb.Author.AnyAsync(a=>a.Name== request.Author.Name, cancellationToken))
            {
                throw new ArgumentException();
            }
            contentGuessDb.Author.Add(request.Author);
            await contentGuessDb.SaveChangesAsync(cancellationToken);
            return request.Author;
            
        }
    }
}
