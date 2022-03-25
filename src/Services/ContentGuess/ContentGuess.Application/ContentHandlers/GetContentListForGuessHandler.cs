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
    public class ContentListForGuessQuery : IRequest<List<ContentForGuess>>
    {
        public ContentListForGuessQuery(int count, int falseNamesCount)
        {
            ContentCount = count;
            FalseNamesCount = falseNamesCount;
        }
        public int ContentCount { get; set; }
        public int FalseNamesCount { get; set; }
        public List<int>? TagIds { get; set; }
        public int? ContentType { get; set; }
    }
    public class GetContentListForGuessHandler : IRequestHandler<ContentListForGuessQuery, List<ContentForGuess>>
    {
        private readonly IRequestHandler<ContentListQuery, List<ContentRead>> contentListQueryHandler;
        private readonly IContentGuessDbContext contentGuessDbContext;

        public GetContentListForGuessHandler(IRequestHandler<ContentListQuery, List<ContentRead>> contentListQueryHandler,
            IContentGuessDbContext contentGuessDbContext)
        {
            this.contentListQueryHandler = contentListQueryHandler;
            this.contentGuessDbContext = contentGuessDbContext;
        }
        public async Task<List<ContentForGuess>> HandleAsync(ContentListForGuessQuery request, CancellationToken cancellationToken)
        {
            return null;
            //    var contentCollection = await contentListQueryHandler.HandleAsync(new ContentListQuery(request.ContentCount)
            //    {
            //        ContentType = request.ContentType,
            //        TagIds = request.TagIds,
            //        NeedShuffle = true
            //    }, cancellationToken) ;
            //    var contentForGuessList = new List<ContentForGuess>();
            //    var query = contentGuessDbContext.Contents.AsQueryable();
            //    if (request.TagIds?.Count > 0)
            //        query = query.Include(t => t.Tags).Where(t => t.Tags.Any(tag => request.TagIds.Contains(tag.Id)));
            //    var falseNames = await query.Include(c => c.ContentInfo).ThenInclude(a => a.Author)
            //      .Select(c => new { Name = c.ContentInfo.AuthorId != null ? $"{c.Name} |by {c.ContentInfo.Author!.Name}" : c.Name, c.Id })
            //      .Take(request.FalseNamesCount * request.ContentCount).OrderBy(c => Guid.NewGuid()).ToListAsync();
            //    foreach (var content in contentCollection)
            //    {
            //        contentForGuessList.Add(new ContentForGuess(content, falseNames.Where(c => c.Id != content.Id).Select(c => c.Name).OrderBy(c => Guid.NewGuid()).Take(request.FalseNamesCount), content.ContentStartSeconds));
            //    }

            //    return contentForGuessList;
            //}
        }
    }
}
