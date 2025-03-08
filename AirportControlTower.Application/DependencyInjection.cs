using AirportControlTower.Application.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;

namespace AirportControlTower.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddHostedService<GroundCrewJob>();

        return services;
    }
}
