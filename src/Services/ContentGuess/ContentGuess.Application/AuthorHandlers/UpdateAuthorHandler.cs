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
    public class UpdateAuthorRequest : IRequest<Author>
    {
        public UpdateAuthorRequest(int id,Author author)
        {
            Id = id;
            Author = author;
        }
        public int Id { get; set; }
        public Author Author { get; set; }
    }
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorRequest, Author>
    {
        private readonly IContentGuessDbContext contentGuessDb;

        public UpdateAuthorHandler(IContentGuessDbContext contentGuessDb)
        {
            this.contentGuessDb = contentGuessDb;
        }
        public async Task<Author> HandleAsync(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            var author = await contentGuessDb.Author.SingleAsync(a => a.Id == request.Id);
            author.Name = request.Author.Name;
            await contentGuessDb.SaveChangesAsync(cancellationToken);
            return author;
        }
    }
}
