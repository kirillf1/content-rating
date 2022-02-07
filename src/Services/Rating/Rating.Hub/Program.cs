
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Rating.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RatingDbContext>(options =>
           options.UseNpgsql(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies").AddCookie("Cookies", "Cookies", options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
