using Rating.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Application.Dto
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            Name = user.Name;
            Id = user.Id;
            Email = user.Email;
        }
        public UserDTO(int id, string name, string email)
        {
            Id = id; 
            Name = name;
            Email = email;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
