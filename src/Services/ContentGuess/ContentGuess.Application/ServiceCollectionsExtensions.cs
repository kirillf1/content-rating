using ContentGuess.Application.AuthorHandlers;
using ContentGuess.Application.AuthorHandlers.Decorators;
using ContentGuess.Application.ContentHandlers;
using ContentGuess.Application.Dto;
using ContentGuess.Application.TagHandlers;
using ContentGuess.Application.TagHandlers.Decorators;
using ContentGuess.Domain;
using ContentGuess.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace ContentGuess.Application
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            AddTagHandlers(serviceCollection);
            AddContentHandlers(serviceCollection);
            AddAuthorHandlers(serviceCollection);
            return serviceCollection;

        }
        private static void AddTagHandlers(IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddScoped<GetTagListQueryHandler>();
            serviceCollection.AddScoped<IRequestHandler<GetTagRequest, TagRead?>, GetTagQueryHandler>();
            serviceCollection.AddScoped<IRequestHandler<GetTagListRequest, List<TagRead>>>(s =>
            {
                return new GetTagListDecorator(s.GetRequiredService<GetTagListQueryHandler>(), s.GetRequiredService<IMemoryCache>());
            });


            serviceCollection.AddScoped<IRequestHandler<AddTagRequest, Tag?>,AddTagHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateTagRequest, Tag>,UpdateTagHandler>();
           
        }
        private static void AddContentHandlers(IServiceCollection serviceCollection)
        {
            //queries
            serviceCollection.AddScoped<IRequestHandler<ContentListForGuessQuery, List<ContentForGuess>>, GetContentListForGuessHandler>();
            serviceCollection.AddScoped<IRequestHandler<ContentListQuery, List<ContentRead>>, GetContentListQueryHandler>();
            serviceCollection.AddScoped<IRequestHandler<GetContentRequest, ContentWrite>, GetContentUpdateQueryHandler>();

            //commands
            serviceCollection.AddScoped<IRequestHandler<AddContentRequest, List<Content>>, AddContentHandler>();
            serviceCollection.AddScoped<IRequestHandler<DeleteContentRequest, bool>, DeleteContentHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateContentRequest, Content>, UpdateContentHandler>();
        }
        private static void AddAuthorHandlers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<AddAuthorHandler>();
            serviceCollection.AddScoped<UpdateAuthorHandler>();
            serviceCollection.AddScoped<GetAuthorsQueryHandler>();
            serviceCollection.AddScoped<IRequestHandler<AddAuthorRequest, Author>>(c => 
            {
                return new UpdateAuthorChacheDecorator<AddAuthorRequest>(c.GetRequiredService<AddAuthorHandler>(), c.GetRequiredService<IMemoryCache>());
            });
            serviceCollection.AddScoped<IRequestHandler<UpdateAuthorRequest, Author>>(c =>
            {
                return new UpdateAuthorChacheDecorator<UpdateAuthorRequest>(c.GetRequiredService<UpdateAuthorHandler>(), c.GetRequiredService<IMemoryCache>());
            });
            serviceCollection.AddScoped<IRequestHandler<GetAuthorsQueryRequest, List<Author>>>(c =>
            {
                return new GetAuthorsQueryCacheDecorator(c.GetRequiredService<GetAuthorsQueryHandler>(), c.GetRequiredService<IMemoryCache>());
            });
            
        }

    }
}
