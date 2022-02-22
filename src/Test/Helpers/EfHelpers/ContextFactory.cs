using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Rating.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers.EfHelpers
{
    public static class ContextFactory
    {
        public static RatingDbContext CreateInMemoryContext()
        {
            var name = $"{DateTime.Now.ToFileTimeUtc()}";
            return new RatingDbContext(new DbContextOptionsBuilder<RatingDbContext>()
                   .UseInMemoryDatabase(name, new InMemoryDatabaseRoot())
                   .Options);
        }
    }
}
