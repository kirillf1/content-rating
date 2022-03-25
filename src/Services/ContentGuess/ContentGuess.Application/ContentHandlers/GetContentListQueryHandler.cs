using ContentGuess.Application.Dto;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.ContentHandlers
{
    public class ContentListQuery: IRequest<List<ContentRead>>
    {
        public ContentListQuery(IEnumerable<long> contentIds)
        {
            ContentIds = new List<long>(contentIds);
            
        }
        public string? OrderColumn { get; set; }
        public List<long> ContentIds { get; set; }
    }
    public class GetContentListQueryHandler : IRequestHandler<ContentListQuery, List<ContentRead>>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public GetContentListQueryHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<List<ContentRead>> HandleAsync(ContentListQuery request, CancellationToken cancellationToken)
        {

            var query = contentGuessDbContext.Contents.Include(c=>c.ContentInfo).ThenInclude(a=>a.Author).Include(c=>c.ContentType).AsQueryable();
            if ( request.ContentIds.Count > 0)
            {
                query = query.Where(c => request.ContentIds.Contains(c.Id));
            }
            if (request.OrderColumn != null)
            {
                var column = request.OrderColumn.ToUpper();
                switch (column)
                {
                    case "ID":
                        query = query.OrderBy(i => i.Id);
                        break;
                    case "NAME":
                        query = query.OrderBy(c => c.Name);
                        break;
                    default:
                        break;
                }
            }
            var contentRead = query.Select(c => new ContentRead(c.Id,
               c.ContentInfo.AuthorId != null ? $"{c.Name} |by {c.ContentInfo.Author!.Name}" : c.Name,
                c.ContentInfo.Url, c.ContentType.Name)
            {
                ContentStartSeconds = c.ContentInfo.StartTimeSeconds
            });
            return await contentRead.ToListAsync();
        }
    }
}
