using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Domain
{
    public class ContentInfo
    {
        public ContentInfo()
        {

            Author = new Author("unknown");
        }
        public ContentInfo(string url)
        {
            Url = url;
           
        }
        public void AddAuthor(Author author)
        {
            AuthorId = author.Id;
            Author = author;
        }
        public double StartTimeSeconds { get; set; }
        public int Id { get; set; }
        public string Url { get; set; } = default!;
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
