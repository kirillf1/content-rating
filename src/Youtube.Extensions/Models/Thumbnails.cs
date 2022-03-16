using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.Extensions.Models
{
    public class Thumbnails
    {
        public Default? Default { get; set; } = default!;
        public Default? Medium { get; set; } = default!;
        public Default? High { get; set; } = default!;
        public Default? Standard { get; set; } = default!;
        public Default? Maxres { get; set; } = default!;
    }
}
