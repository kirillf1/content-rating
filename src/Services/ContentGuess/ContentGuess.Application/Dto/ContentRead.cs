using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.Dto
{
    public class ContentRead
    {
        public ContentRead(long id,string name, string url,string contentType)
        {
         
            Url = url;
            Id = id;
            Name = name;
            ContentType = contentType;
        }
        public string Name { get; set; }
        public string Url { get; set; }
        public long Id { get; set; }
        public string ContentType { get; set; }
        public double ContentStartSeconds { get; set; }
    }
}
