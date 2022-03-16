using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Web.Client;
using Web.Client.Http;
using Web.Shared;
using Web.Shared.Authentication;
using Web.Shared.GuessContent;
using Web.Shared.Rooms;
using Youtube.Extensions.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<UserService>(s => 
{ 
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("api");
    return new UserService(client);
});
builder.Services.AddTransient<RoomService>(s =>
{
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("api");
    return new RoomService(client);
});
builder.Services.AddTransient<TagService>(s =>
 {
     var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("contentGuessApi");
     return new TagService(client);
 });
builder.Services.AddTransient<ContentGuessService>(s =>
{
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("contentGuessApi");
    return new ContentGuessService(client);
});
builder.Services.AddTransient<AuthorService>(s =>
{
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("contentGuessApi");
    return new AuthorService(client);
});
builder.Services.AddTransient<ContentTypeService>(s =>
{
    var client = s.GetRequiredService<IHttpClientFactory>().CreateClient("contentGuessApi");
    return new ContentTypeService(client);
});
var configuration = builder.Configuration;
builder.Services.AddHttpClient("contentGuessApi", s => { s.BaseAddress = new Uri(configuration.GetSection("Urls").GetSection("ContentGuessApi").Value); });
builder.Services.AddHttpClient("api", s => { s.BaseAddress = new Uri(configuration.GetSection("Urls").GetSection("ApiAddress").Value); } ).AddHttpMessageHandler<CookieHttpClientHandler>();
builder.Services.AddTransient<CookieHttpClientHandler>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<ApiAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<ApiAuthenticationStateProvider>());
builder.Services.AddScoped<RatingClientHub>(s => { return new RatingClientHub(configuration.GetSection("Urls").GetSection("RoomRatingHub").Value); });
builder.Services.AddScoped<RoomListClientHub>(s => { return new RoomListClientHub(configuration.GetSection("Urls").GetSection("RoomListHub").Value); });
builder.Services.AddSingleton<State>();
builder.Services.AddScoped<PlaylistService>();
await builder.Build().RunAsync();
