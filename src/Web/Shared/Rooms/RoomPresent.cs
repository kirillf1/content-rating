using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Rooms
{
    public class RoomPresent
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreationTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
