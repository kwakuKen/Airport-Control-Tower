﻿using AirportControlTower.Domain.Interfaces;
using AirportControlTower.Infrastructure.BackgroundJobs;
using AirportControlTower.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddHostedService<GetWeatherUpdateJob>();

        services.AddScoped<IAircraftWriteRepository, AircraftRepository>();
        services.AddScoped<IAircraftReadRepository, AircraftRepository>();

        return services;
    }
}
