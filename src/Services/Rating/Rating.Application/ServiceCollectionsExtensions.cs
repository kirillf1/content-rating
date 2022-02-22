using Microsoft.Extensions.DependencyInjection;
using Rating.Application.Dto;
using Rating.Application.Rooms;
using Rating.Application.Users;
using Rating.Domain;
using Rating.Domain.Interfaces;

namespace Rating.Application
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
        {
            AddServices(serviceCollection);
            AddCommands(serviceCollection);
            AddQueries(serviceCollection);
            return serviceCollection;

        }
        private static void AddCommands(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRequestHandler<CreateRoomRequest, Guid>, CreateRoomHandler>();
            serviceCollection.AddScoped<IRequestHandler<JoinRoomRequest, Room>, JoinRoomHandler>();
            serviceCollection.AddScoped<IRequestHandler<LeaveRoomRequest, UserDTO>, LeaveRoomHandler>();
            serviceCollection.AddScoped<IRequestHandler<ChangedContentRating, ChangedContentRating>, UpdateRatingInContentHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateRoomRequest, bool>, UpdateRoomHandler>();
            serviceCollection.AddScoped<IRequestHandler<DeleteRoomRequest, bool>, DeleteRoomHandler>();
            serviceCollection.AddScoped<IRequestHandler<CloseRoomRatingRequest, Guid?>, CloseRoomRatingHandler>();
        }

        private static void AddQueries(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRequestHandler<GetUsersRatingQuery, IList<UsersRating>>, GetUsersRatingQueryHandler>();
            serviceCollection.AddScoped<IRequestHandler<GetRoomQuery, Room>, GetRoomQueryHandler>();
            serviceCollection.AddScoped<IRequestHandler<GetRoomListQuery, List<RoomPresent>>, GetRoomListQueryHandler>();
        }
        private static void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService<UserDTO>, UserService>();
        }
    }
}
