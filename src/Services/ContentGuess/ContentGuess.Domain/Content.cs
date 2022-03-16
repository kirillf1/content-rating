using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Domain
{
    public class Content
    {
        public Content()
        {
            Tags = new List<Tag>();
        }
        public Content(string name, ContentType contentType, ContentInfo contentInfo, IEnumerable<Tag> tags)
        {
            Name = name;
            ContentType = contentType;
            ContentInfo = contentInfo;
            Tags = new List<Tag>();
            Tags.AddRange(tags);
            
        }

        public long Id { get; }
        public ContentType ContentType { get; set; }
        public int ContentTypeId { get; set; }
        public ContentInfo ContentInfo { get; set; }
        public List<Tag> Tags { get; set; } = default!;
        public string Name { get; set; } = default!;
        public void AddTag(Tag tag)
        {
            if(tag.Id==0 || !Tags.Any(t=>t.Id==tag.Id))
            Tags.Add(tag);
        }
        public void AddTags(IEnumerable<Tag> tags)
        {
            foreach (Tag tag in tags)
            {
                AddTag(tag);
            }
        }
    }
    //public enum ContentType
    //{
    //    Video,
    //    Audio,
    //    Photo
    //}
}
