using AirportControlTower.API.Middleware;
using AirportControlTower.Application;
using AirportControlTower.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AirportControlTowerDbContext>();
    await dbContext.SeedParkingSpotsAsync(); 
}

app.UseMiddleware<ApiKeyMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
