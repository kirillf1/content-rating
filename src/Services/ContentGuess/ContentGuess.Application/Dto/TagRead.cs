using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.Dto
{
    public class TagRead
    {
        public TagRead(int id, string name,int? parentId = null)
        {
            Name = name;
            Id = id;
            ParentId = parentId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        
    }
}
