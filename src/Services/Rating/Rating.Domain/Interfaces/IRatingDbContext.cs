using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rating.Domain.Interfaces
{
    public interface IRatingDbContext
    {
        public DbSet<Room> Rooms { get; }
        public DbSet<UserContentRating> UserContentRatings { get;} 
        public DbSet<User> Users { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
