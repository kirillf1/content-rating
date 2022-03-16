using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Domain.Interfaces
{
    public interface IContentGuessDbContext
    {
        public DbSet<Content> Contents { get; }
        public DbSet<Tag> Tags { get; }
        public DbSet<ContentType> ContentType { get; }
        public DbSet<Author> Author { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
