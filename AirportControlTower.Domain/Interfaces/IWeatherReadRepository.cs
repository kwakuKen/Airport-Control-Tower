using AirportControlTower.Domain.Entities;

namespace AirportControlTower.Domain.Interfaces;

public interface IWeatherReadRepository
{
    Task<List<Weather>> GetAllWeatherAsync();
    Task<Weather?> GetWeatherByIdAsync(int id);
}
