using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain
{
    public class User
    {
        public User(string connectionId, string name)
        {
            ConnectionId = connectionId;
            Name = name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ConnectionId { get; private set; }
    }
}
