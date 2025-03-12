using AirportControlTower.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AirportControlTower.Application.Admin.Query.Weather;

internal sealed class WeatherQueryHandler(
    IAdminReadRepository adminReadRepository,
    ILogger<WeatherQueryHandler> logger)
    : IRequestHandler<WeatherQuery, object>
{
    public async Task<object?> Handle(WeatherQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await adminReadRepository.GetCurrentWeatherAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erron in WeatherQueryHandler ");
        }
        return default!;
    }
}
