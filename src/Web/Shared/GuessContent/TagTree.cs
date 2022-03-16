using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.GuessContent
{
    public class TagTree
    {
        public TagTree(Tag tag)
        {
            Tag = tag;
            Children = new List<TagTree>();
        }
        public Tag Tag { get; set; }
        public List<TagTree> Children { get; set; }
    }
}
