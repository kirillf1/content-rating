using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.GuessContent
{
    public class ContentGuessFalseNames
    {
        public ContentGuess Content { get; set; } = default!;
        public List<string> FalseNames { get; set; } = default!;
        public double? ContentStartTime { get; set; }
    }
}
