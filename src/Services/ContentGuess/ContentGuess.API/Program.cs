using ContentGuess.Application;
using ContentGuess.Infrastructure;
using ContentGuess.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(policy =>
        policy.SetIsOriginAllowed(_ => true).AllowCredentials().AllowAnyHeader().AllowAnyMethod());
});
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

