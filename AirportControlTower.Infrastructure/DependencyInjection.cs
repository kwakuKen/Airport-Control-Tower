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
        string? connectionString = configuration.GetConnectionString("PostgresConnection");

        services.AddDbContext<AirportControlTowerDbContext>(options =>
    options.UseNpgsql(connectionString));
        return services;
    }
}
