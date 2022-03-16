using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Domain
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
            Content = new List<Content>();
            ChildTags = new List<Tag>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Content> Content { get; set; }
        public int? ParentTagId { get; set; }
        public Tag? ParentTag { get; set; }
        public List<Tag> ChildTags { get; set; }
    }
}
