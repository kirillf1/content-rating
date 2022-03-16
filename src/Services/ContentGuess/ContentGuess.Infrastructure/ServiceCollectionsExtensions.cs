using ContentGuess.Domain.Interfaces;
using ContentGuess.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ContentGuess.Infrastructure
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnectionString");
            serviceCollection.AddDbContextPool<ContentGuessDbContext>(options => options.UseNpgsql(connectionString));
            serviceCollection.AddScoped<IContentGuessDbContext, ContentGuessDbContext>();
            return serviceCollection;
        }
    }
}
