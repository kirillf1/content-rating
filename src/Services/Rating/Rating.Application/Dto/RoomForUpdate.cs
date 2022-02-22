using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Application.Dto
{
    public class RoomForUpdate
    {

        public string Name { get; set; } = default!;
        public List<ContentDTO> Contents { get; set; } = default!;
        public bool IsCompleted { get; set; }
        public bool IsSingleRoom { get; set; }
        public bool IsPrivate { get; set; }
    }
}
