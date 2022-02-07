using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain
{
    public class User
    {
        public User(string name,string password,string email)
        {
            Name = name;
            Password = password;
            Email = email;
            Rooms = new List<Room>();
        }
        public ICollection<Room> Rooms { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
