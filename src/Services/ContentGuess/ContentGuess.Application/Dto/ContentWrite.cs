using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.Dto
{
    public class ContentWrite
    {
        public ContentWrite()
        {
            
        }
        public ContentWrite(string name, string url, List<int>? tagIds, int contentTypeId, int? authorId, double contentStartSeconds)
        {
            Name = name;
            Url = url;
            TagIds = tagIds;
            ContentTypeId = contentTypeId;
            AuthorId = authorId;
            ContentStartSeconds = contentStartSeconds;
        }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public List<int>? TagIds { get; set; } = default!;
        public int ContentTypeId { get; set; }
        public int? AuthorId { get; set; }
        public double ContentStartSeconds { get; set; }
    }
}
