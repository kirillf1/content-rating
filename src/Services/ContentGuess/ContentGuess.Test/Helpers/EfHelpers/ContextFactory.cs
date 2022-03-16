using ContentGuess.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentGuess.Test.Helpers.EfHelpers
{
    public static class ContextFactory
    {
        public static ContentGuessDbContext CreateInMemoryContext()
        {
            var name = $"{DateTime.Now.ToFileTimeUtc()}";
            return new ContentGuessDbContext(new DbContextOptionsBuilder<ContentGuessDbContext>()
                   .UseInMemoryDatabase(name, new InMemoryDatabaseRoot())
                   .Options);
        }
      
    }
}
