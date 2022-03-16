using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Youtube.Extensions.Models
{
    public class Default
    {
        public string? Url { get; set; } = default!;
        public long Width { get; set; }
        public long Height { get; set; }
    }
}
