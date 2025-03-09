using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IWeatherReadRepository
{
    Task<List<Weather>> GetAllWeatherAsync(CancellationToken cancellationToken);
    Task<Weather?> GetCurrentWeatherAsync(CancellationToken cancellationToken);
    Task<Weather?> GetWeatherByIdAsync(int id, CancellationToken cancellationToken);
}
