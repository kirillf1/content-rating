using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain
{
    public class Content
    {
        public Content(string url)
        {
            Url = url;
        }
        public long Id { get; set; }
        public string Url { get; private set; }
    }
}
