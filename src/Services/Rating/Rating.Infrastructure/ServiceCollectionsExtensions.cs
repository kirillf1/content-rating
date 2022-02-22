using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rating.Domain.Interfaces;
using Rating.Infrastructure.Data;
using Rating.Infrastructure.PasswordHashers;

namespace Rating.Infrastructure
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnectionString");
            serviceCollection.AddDbContext<RatingDbContext>(options => options.UseNpgsql(connectionString));
            serviceCollection.AddScoped<IRatingDbContext, RatingDbContext>();
            serviceCollection.AddTransient<IPasswordHasher, PasswordHasher>();
            return serviceCollection;
        }
    }
}
