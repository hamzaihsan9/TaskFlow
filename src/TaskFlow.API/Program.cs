using TaskFlow.API.Middleware;
using TaskFlow.Application;
using TaskFlow.Infrastructure;
using TaskFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

// ============================================================
// Program.cs — Application Entry Point
// ============================================================
// This is where everything comes together:
// 1. Register services from each layer
// 2. Configure the HTTP pipeline (middleware)
// 3. Start the application
// ============================================================

var builder = WebApplication.CreateBuilder(args);

// --- Service Registration (Dependency Injection) ---

// Add services from the Application layer (MediatR, FluentValidation)
builder.Services.AddApplicationServices();

// Add services from the Infrastructure layer (EF Core, Repositories)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add API services
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TaskFlow API",
        Version = "v1",
        Description = "A professional task management API built with Clean Architecture"
    });
});

var app = builder.Build();

// --- Middleware Pipeline ---
// Order matters! Requests flow top-to-bottom, responses bottom-to-top.

// Global error handling (catches all unhandled exceptions)
app.UseMiddleware<ExceptionMiddleware>();

// Swagger UI (available at /swagger)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow API v1");
    c.RoutePrefix = "swagger";
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

// --- Database Initialization ---
// Automatically create/migrate the database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TaskFlowDbContext>();
    db.Database.EnsureCreated();
}

Console.WriteLine(@"
╔═════════════════════════════════════════════════════╗
║             TaskFlow API is running!                ║
║                                                     ║
║  Swagger UI: http://localhost:5000/swagger          ║
║  API Base:   http://localhost:5000/api              ║
╚═════════════════════════════════════════════════════╝
");

app.Run();
