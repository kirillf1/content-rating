using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rating.Domain
{
    public class User
    {
        /// <summary>
        /// Create fake user without password and email
        /// </summary>
        /// <param name="name"></param>
        public User(string name)
        {
            Name = name;
            UserType = UserType.Fake;
            Email = "";
            Password = "";
            Rooms = new List<Room>();
            RatedContent = new List<UserContentRating>();
        }
        public User(string name,string password,string email)
        {
            Name = name;
            Password = password;
            Email = email;
            Rooms = new List<Room>();
            UserType = UserType.Real;
            RatedContent = new List<UserContentRating>();
        }
        [JsonIgnore]
        public ICollection<Room> Rooms { get; set; }
        [JsonIgnore]
        public List<UserContentRating> RatedContent { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

    }
    public enum UserType
    {
        Real,
        Fake
    }
}
