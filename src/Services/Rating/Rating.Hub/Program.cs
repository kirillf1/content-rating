using Microsoft.EntityFrameworkCore;
using Rating.Application;
using Rating.Hub.Hubs;
using Rating.Infrastructure;
using Rating.Infrastructure.Data;

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
await EnsureDbAsync(app.Services);
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

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<RatingDbContext>();
    await db.Database.MigrateAsync();
}