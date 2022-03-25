using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.Dto
{
    public class ContentInformation
    {
        public ContentInformation(long id,string name)
        {
            Id = id;
            Name = name;
        }
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
