using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain.Interfaces
{
    public interface IUserService <TUser>
    {
        public Task<TUser?> Register(string username, string password,string email);
        public Task<TUser?> GetUser(string username, string password);
        public Task<TUser?> GetUserByName(string username);
        public Task<bool> DeleteUser(string username, string password);
        public Task<IEnumerable<TUser>> GetAllUsers();
        /// <summary>
        /// Checks duplicate name if exists returns true 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<bool> CheckDuplicateName(string username);
    }
}
