using Microsoft.EntityFrameworkCore;
using RedisCachingDemo.Data;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // This will use the property names as defined in the C# model
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

// Configure DbContext to Use SQL Server Database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Redis distributed cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    //This property is set to specify the connection string for Redis
    //The value is fetched from the application's configuration system, i.e., appsettings.json file
    options.Configuration = builder.Configuration["RedisCacheOptions:Configuration"];
    //This property helps in setting a logical name for the Redis cache instance. 
    //The value is also fetched from the appsettings.json file
    options.InstanceName = builder.Configuration["RedisCacheOptions:InstanceName"];
});
// Register the Redis connection multiplexer as a singleton service
// This allows the application to interact directly with Redis for advanced scenarios

// Establish a connection to the Redis server using the configuration from appsettings.json
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(builder.Configuration["RedisCacheOptions:Configuration"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
