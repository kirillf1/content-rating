using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared
{
    public class State
    {
        private User? user;
        public User? User 
        { get { return user; } 
          set 
            { 
                user = value; 
                UserChanged?.Invoke(user); 
            } 
        }
        public Action<User?>? UserChanged;
    }
}
