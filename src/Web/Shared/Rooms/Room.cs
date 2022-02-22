using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Rooms
{
    public class Room
    {
        public Room()
        {

        }
        public Room(string name)
        {
            Name = name;
            Users = new List<User>();
            Contents = new List<Content>();
        }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; } = default!;
        [ValidateComplexType]
        public List<User> Users { get; set; } = default!;
        [ValidateComplexType]
        public List<Content> Contents { get; set; } = default!;
        public bool IsSingleRoom { get; set; }
        public int CreatorId { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsPrivate { get; set; }
    }
}
