using Application;
using DotNetEnv;
using Infrastructure;
using WebApi;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }