using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.GuessContent
{
    public class ContentGuess
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public string ContentType { get; set; } = "YoutubeVideo";
        public double ContentStartSeconds { get; set; }
    }
}
