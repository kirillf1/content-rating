using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.ContentHandlers
{
    public class DeleteContentRequest: IRequest<bool>
    {
        public long ContentId { get; set; }
    }
    public class DeleteContentHandler : IRequestHandler<DeleteContentRequest, bool>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public DeleteContentHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<bool> HandleAsync(DeleteContentRequest request, CancellationToken cancellationToken)
        {
            var content = await contentGuessDbContext.Contents.FirstOrDefaultAsync(c => request.ContentId == c.Id);
            if (content == null)
                return false;
            contentGuessDbContext.Contents.Remove(content);
            await contentGuessDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
