using AirportControlTower.Domain.Interfaces;
using MediatR;

namespace AirportControlTower.Application.Weather.Query;

internal sealed class WeatherQueryHandler(IWeatherReadRepository weatherReadRepository)
    : IRequestHandler<WeatherQuery, WeatherQueryResult>
{
    public async Task<WeatherQueryResult> Handle(WeatherQuery request, CancellationToken cancellationToken)
    {       
        var response = await weatherReadRepository.GetCurrentWeatherAsync(cancellationToken);
        if (response is null)
            return default!;
        var wind = new WindInfo(
                Math.Round(response.WindSpeed, 1),
                response.WindDirection
                );
        return new WeatherQueryResult(
            response.Description!,
            Math.Round(response.Temperature, 1),
            response.Visibility,
           wind,
            response.CreatedAt
            );
    }
}
