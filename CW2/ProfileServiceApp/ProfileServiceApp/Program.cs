var builder = WebApplication.CreateBuilder(args);

// Create a new instance of the WebApplicationBuilder to configure the application.

// Add services to the container.
builder.Services.AddControllers(); // Add controller services to the dependency injection container.

// Add services for API documentation using Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer(); // Register API endpoint explorer.
builder.Services.AddSwaggerGen(); // Register Swagger generator for API documentation.

var app = builder.Build(); // Build the application using the configured services and settings.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger middleware for generating API documentation.
    app.UseSwaggerUI(); // Enable Swagger UI for interactive API documentation in development mode.
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS.

app.UseAuthorization(); // Enable authorization handling.

app.MapControllers(); // Map controllers to handle incoming HTTP requests.

app.Run(); // Start the application and listen for incoming HTTP requests.
