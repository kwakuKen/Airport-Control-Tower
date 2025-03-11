using AirportControlTower.Application.BackgroundJobs;
using AirportControlTower.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace AirportControlTower.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddSingleton<IHostedService, GroundCrewJob>();
        services.AddScoped<IFlightService, FlightService>();


        return services;
    }
}
