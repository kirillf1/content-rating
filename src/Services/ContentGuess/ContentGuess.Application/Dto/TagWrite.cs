using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Application.Dto
{
    public class TagWrite
    {
        public string Name { get; set; } = default!;
        public int? ParentId { get; set; }
    }
}
