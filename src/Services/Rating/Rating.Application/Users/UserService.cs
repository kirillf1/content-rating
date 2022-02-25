using Microsoft.EntityFrameworkCore;
using Rating.Application.Dto;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application.Users
{
    public class UserService : IUserService<UserDTO>
    {
        private readonly IRatingDbContext ratingDbContext;
        private readonly IPasswordHasher passwordHasher;

        public UserService(IRatingDbContext ratingDbContext, IPasswordHasher passwordHasher)
        {
            this.ratingDbContext = ratingDbContext;
            this.passwordHasher = passwordHasher;
        }
        /// <summary>
        /// Checks duplicate name if exists returns true 
        /// </summary>
        public async Task<bool> CheckDuplicateName(string username)
        {
            var duplicateUserName = await ratingDbContext.Users.Select(c=>c.Name).FirstOrDefaultAsync(u => u == username);
            return duplicateUserName != null;
        }

        public async Task<bool> DeleteUser(string username, string password)
        {
            var user = await ratingDbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null || !passwordHasher.Check(user!.Password, password).Verified)
                return false;
            ratingDbContext.Users.Remove(user);
            await ratingDbContext.SaveChangesAsync(default);
            return true;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await ratingDbContext.Users.Where(u => u.UserType != Domain.UserType.Fake).Select(u=>new UserDTO(u)).ToListAsync();
        }
        /// <summary>
        /// Add new user in context and hashing password. If username exists return null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserDTO?> Register(string username, string password, string email)
        {

            if (await CheckDuplicateName(username))
                return default;
             password = passwordHasher.Hash(password);
             var user = new User(username, password, email);
             ratingDbContext.Users.Add(user);
             await ratingDbContext.SaveChangesAsync(default);
             return new UserDTO(user);
            
        }

        /// <summary>
        /// if user exists return User else default value
        /// </summary>
        public async Task<UserDTO?> GetUserByName(string username)
        {
            var user = await ratingDbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null)
                return default;
            return new UserDTO(user);
        }

        public async Task<UserDTO?> GetUser(string username, string password)
        {
            password = passwordHasher.Hash(password);
            var user = await ratingDbContext.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user == null || !passwordHasher.Check(user!.Password,password).Verified)
                return default;
            return new UserDTO(user);
        }
    }
   
}
