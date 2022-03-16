using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Shared.Rooms;

namespace Web.Shared.GuessContent
{
    public class ContentGuessEdit
    {


        public ContentGuessEdit()
        {
            Name = String.Empty;
            TagIds = new List<int>();
            Url = String.Empty;
            ContentStartSeconds = 0;
        }
        public ContentGuessEdit(string name, string url, List<int>? tagIds, int contentTypeId, int? authorId, double? contentStartSeconds)
        {
            Name = name;
            Url = url;
            TagIds = tagIds;
            ContentTypeId = contentTypeId;
            AuthorId = authorId;
            ContentStartSeconds = contentStartSeconds.GetValueOrDefault();
        }
        public string Url { get => _url; set => _url = UrlConverter.Convert(value); }
        private string _url = default!;
        public string Name { get; set; } = default!;
        public List<int>? TagIds { get; set; } = default!;
        public int ContentTypeId { get; set; }
        public int? AuthorId { get; set; }
        public double? ContentStartSeconds { get; set; }
    }
}
