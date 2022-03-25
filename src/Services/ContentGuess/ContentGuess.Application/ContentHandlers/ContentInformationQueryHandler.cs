using ContentGuess.Application.Dto;
using ContentGuess.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.ContentHandlers
{
    public class ContentInformationRequest : IRequest<IEnumerable<ContentInformation>>
    {
        public ContentInformationRequest(int count)
        {
           ContentCount = count;
           
        }
        public string? OrderColumn { get; set; }
        public int ContentCount { get; set; }
        public bool? NeedShuffle { get; set; }
        public int? ContentType { get; set; }
        public List<int>? TagIds { get; set; }

    }
    public class ContentInformationQueryHandler : IRequestHandler<ContentInformationRequest, IEnumerable<ContentInformation>>
    {
        private readonly IContentGuessDbContext contentGuessDbContext;

        public ContentInformationQueryHandler(IContentGuessDbContext contentGuessDbContext)
        {
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<IEnumerable<ContentInformation>> HandleAsync(ContentInformationRequest request, CancellationToken cancellationToken)
        {
            var query = contentGuessDbContext.Contents.Include(c => c.ContentInfo).ThenInclude(c => c.Author).AsQueryable();
            if (request.TagIds?.Count > 0)
            {
                query = query.Include(c=>c.Tags).Where(c => c.Tags.Any(c => request.TagIds!.Contains(c.Id)));
            }
            if (request.ContentType != null)
            {
                query = query.Where(c => c.ContentTypeId == request.ContentType);
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
            else if (request.NeedShuffle.GetValueOrDefault())
            {
               query = query.OrderBy(c => Guid.NewGuid());
            }
            if(request.ContentCount > 0)
                query = query.Take(request.ContentCount);
            return await query.
                Select(c => new ContentInformation(c.Id,c.ContentInfo.AuthorId != null ? $"{c.Name} |by {c.ContentInfo.Author!.Name}" : c.Name )).ToListAsync();
            
            

        }
    }
}
