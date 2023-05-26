using SwissChatApi.Authorization;
using SwissChatApi.Entities;
using SwissChatApi.Helpers;
using SwissChatApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var env = builder.Environment;
// use sql server db in production and sqlite db in development
if (env.IsProduction())
    services.AddDbContext<SwissDBContext>();
else
    services.AddDbContext<SwissDBContext, SqliteSwissDBContext>();

services.AddControllers();

// configure automapper with all automapper profiles from this assembly
services.AddAutoMapper(typeof(Program));

// configure DI for application services
services.AddScoped<IJwtUtils, JwtUtils>();
services.AddScoped<IUserService, UserService>();
// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
