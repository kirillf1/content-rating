using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Application.Dto
{
    public class RoomPresent
    {
        public RoomPresent(Guid id, string name, DateTime creationTime, bool isCompleted)
        {
            Id = id;
            Name = name;
            CreationTime = creationTime;
            IsCompleted = isCompleted;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsCompleted { get; set; }
    }
}
