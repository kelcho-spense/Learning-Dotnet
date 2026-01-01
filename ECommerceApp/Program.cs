using ECommerceOrderManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // This will use the property names as defined in the C# model
        //options.JsonSerializerOptions.PropertyNamingPolicy = null;

        // This makes JSON property name matching case-insensitive ie "customerId" or "CustomerID" will bind to CustomerId
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

// Register DbContext and Connection String
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSwaggerGen(options =>
{
    // Define API metadata (title, version, description, contact) shown in Swagger UI
    options.SwaggerDoc("v1", new()
    {
        Title = "E-Commerce Order Management API",
        Version = "v1",
        Description = "API for managing e-commerce orders, products, customers, and order processing operations.",
        Contact = new()
        {
            Name = "API Support",
            Email = "support@ecommerceordermanagement.com",
            Url = new Uri("https://www.ecommerceordermanagement.com/support")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as JSON endpoint
    app.UseSwagger();
    // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
    // Provides interactive documentation at /swagger
    app.UseSwaggerUI(options =>
    {
        // Configure the JSON endpoint for Swagger UI to consume
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce Order Management API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
