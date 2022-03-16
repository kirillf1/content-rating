using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.GuessContent
{
    public class Tag
    {
        public string Name { get; set; } = default!;
        public int Id { get; set; } = default!;
        public int? ParentId { get; set; }
    }
}
