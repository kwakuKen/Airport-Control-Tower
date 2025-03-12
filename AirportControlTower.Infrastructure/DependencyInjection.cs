using AirportControlTower.Domain.Interfaces;
using AirportControlTower.Infrastructure.BackgroundJobs;
using AirportControlTower.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AirportControlTower.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
         IConfiguration configuration)
    {
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
            ?? configuration.GetConnectionString("PostgresConnection");

        services.AddDbContext<AirportControlTowerDbContext>(options =>
            options.UseNpgsql(connectionString));

        using (var serviceProvider = services.BuildServiceProvider())
        {
            var context = serviceProvider.GetRequiredService<AirportControlTowerDbContext>();
            context.Database.Migrate();
        }
       

        services.AddSingleton<IHostedService, GetWeatherUpdateJob>();

        services.AddScoped<IAircraftWriteRepository, AircraftRepository>();
        services.AddScoped<IAircraftReadRepository, AircraftRepository>();
        services.AddScoped<IWeatherWriteRepository, WeatherRepository>();
        services.AddScoped<IWeatherReadRepository, WeatherRepository>();
        services.AddScoped<IGroundCrewRepository, GroundCrewRepository>();
        services.AddScoped<IAdminReadRepository, AdminRepository>();



        return services;
    }
}
