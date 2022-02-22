using Rating.Application;
using Rating.Hub.Hubs;
using Rating.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(policy =>
        policy.SetIsOriginAllowed(_ => true).AllowCredentials().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies").AddCookie("Cookies", "Cookies", options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{

}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<RoomHub>("/room");
app.MapHub<RoomListHub>("/roomList");
app.MapControllers();
app.Run();
